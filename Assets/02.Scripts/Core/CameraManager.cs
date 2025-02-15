using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera playerCam;
    public CinemachineCamera closetLockCam;
    public CinemachineCamera toiletCam;


    public void ViewChange(CinemachineCamera target)
    {
        target.Prioritize();
    }

    public void PlayerView()=>playerCam.Prioritize();
}
