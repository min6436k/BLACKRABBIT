using UnityEngine;

public class Note : CloseUpInteractableObject
{
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.cameraManager.ViewChange(GameManager.Instance.cameraManager.noteCam);

    }

    public override bool IsInteractionPossible()
    {
        return true;
    }
}
