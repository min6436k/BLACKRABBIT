using UnityEngine;

public class SmallRoomDoor : Door
{
    public override bool IsInteractionPossible()
    {
        return GameManager.Instance.gameFlags.isUnlockCloset;
    }
}
