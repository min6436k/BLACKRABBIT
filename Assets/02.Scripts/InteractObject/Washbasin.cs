using UnityEngine;

public class Washbasin : InteractableObject
{
    public override void Interact()
    {
    }

    public override bool IsInteractionPossible()
    {
        return GameManager.Instance.gameFlags.isMirrorBroken;
    }
}
