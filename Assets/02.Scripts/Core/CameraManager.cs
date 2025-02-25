using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public CinemachineCamera playerCam;
    public CinemachineCamera closetLockCam;
    public CinemachineCamera exitDoorLockCam;
    public CinemachineCamera noteCam;
    public CinemachineCamera toiletCam;
    public CinemachineCamera washbasinCam;
    public CinemachineCamera tvCam;

    private List<CinemachineCamera> _nonPlayerCameraList = new List<CinemachineCamera>();
    private LiftGammaGain LFG;
    private ColorAdjustments colorAdjustments;
    private bool _isRedScreen = false;

    private void Start()
    {
        _nonPlayerCameraList.Add(closetLockCam);
        _nonPlayerCameraList.Add(exitDoorLockCam);
        _nonPlayerCameraList.Add(toiletCam);
        _nonPlayerCameraList.Add(noteCam);
        _nonPlayerCameraList.Add(washbasinCam);
        mainCamera.GetComponent<Volume>().profile.TryGet<LiftGammaGain>(out LFG);
        mainCamera.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out colorAdjustments);
    }

    public void ViewChange(CinemachineCamera target)
    {
        _nonPlayerCameraList.ForEach(x => x.gameObject.SetActive(false));
        target.gameObject.SetActive(true);
        target.Prioritize();
    }

    public void PlayerView() => ViewChange(playerCam);

    public void RedScreen()
    {
        if (!_isRedScreen)
        {
            StartCoroutine(OnJumpScare());
        }
        else
        {
            LFG.active = false;
        }

        _isRedScreen = !_isRedScreen;
    }
    
    public void EndingLight()
    {
        DOTween.To(() => colorAdjustments.postExposure.value, x => colorAdjustments.postExposure.value = x, 25, 2)
            .SetEase(Ease.InQuart).SetDelay(2);
    }

    IEnumerator OnJumpScare()
    {
        for (int i = 0; i < 6; i++)
        {
            LFG.active = true;
            yield return new WaitForSeconds(0.5f/6);
            LFG.active = false;
            yield return new WaitForSeconds(0.5f/6);
        }
        LFG.active = true;
    }


    
}