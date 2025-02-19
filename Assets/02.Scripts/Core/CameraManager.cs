using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera playerCam;
    public CinemachineCamera closetLockCam;
    public CinemachineCamera exitDoorLockCam;
    public CinemachineCamera noteCam;
    public CinemachineCamera toiletCam;

    private List<CinemachineCamera> _nonPlayerCameraList = new List<CinemachineCamera>();

    private void Start()
    {
        _nonPlayerCameraList.Add(closetLockCam);
        _nonPlayerCameraList.Add(exitDoorLockCam);
        _nonPlayerCameraList.Add(toiletCam);
        _nonPlayerCameraList.Add(noteCam);
    }

    public void ViewChange(CinemachineCamera target)
    {
        _nonPlayerCameraList.ForEach(x=>x.gameObject.SetActive(false));
        target.gameObject.SetActive(true);
        target.Prioritize();
    }

    public void PlayerView() => ViewChange(playerCam);
}
