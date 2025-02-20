using UnityEngine;

public class SmallRoomDoor : Door
{
    private bool wasOpened;

    public override void Interact()
    {
        base.Interact();
        if (!wasOpened)
        {
            wasOpened = true;
            GameManager.Instance.eventManager.SmallRoomDoorOpenEvent.Invoke();
        }
    }
    
    public override bool IsInteractionPossible()
    {
        return GameManager.Instance.gameFlags.isUnlockCloset;
    }
}
