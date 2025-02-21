using System;
using System.Collections;
using System.Collections.Generic;
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
    public CinemachineCamera tvCam;

    private List<CinemachineCamera> _nonPlayerCameraList = new List<CinemachineCamera>();
    private LiftGammaGain LFG;

    private void Start()
    {
        _nonPlayerCameraList.Add(closetLockCam);
        _nonPlayerCameraList.Add(exitDoorLockCam);
        _nonPlayerCameraList.Add(toiletCam);
        _nonPlayerCameraList.Add(noteCam);
        mainCamera.GetComponent<Volume>().profile.TryGet<LiftGammaGain>(out LFG);
    }

    public void ViewChange(CinemachineCamera target)
    {
        _nonPlayerCameraList.ForEach(x => x.gameObject.SetActive(false));
        target.gameObject.SetActive(true);
        target.Prioritize();
    }

    public void PlayerView() => ViewChange(playerCam);

    public void JumpScareLight(bool b)
    {
        if (b)
        {
            StartCoroutine(OnJumpScare());
        }
        else
        {
            LFG.active = false;
        }
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