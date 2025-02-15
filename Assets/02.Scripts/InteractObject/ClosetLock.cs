using UnityEngine;

public class ClosetLock : InteractableObject
{
    public override void Interact()
    {
        GameManager.Instance.playerController.CurrentState = PlayerState.Interact;
        GameManager.Instance.cameraManager.ViewChange(GameManager.Instance.cameraManager.closetLockCam);
    }

    public override bool IsInteractionPossible()
    {
        return true;
    }
}
