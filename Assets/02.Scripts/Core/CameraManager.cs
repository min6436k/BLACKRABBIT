using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera playerCam;
    public CinemachineCamera closetLockCam;
    public CinemachineCamera exitDoorLockCam;
    public CinemachineCamera toiletCam;

    private List<CinemachineCamera> _nonPlayerCameraList = new List<CinemachineCamera>();

    private void Start()
    {
        _nonPlayerCameraList.Add(closetLockCam);
        _nonPlayerCameraList.Add(exitDoorLockCam);
        _nonPlayerCameraList.Add(toiletCam);
    }

    public void ViewChange(CinemachineCamera target)
    {
        _nonPlayerCameraList.ForEach(x=>x.gameObject.SetActive(false));
        target.gameObject.SetActive(true);
        target.Prioritize();
    }

    public void PlayerView() => ViewChange(playerCam);
}
