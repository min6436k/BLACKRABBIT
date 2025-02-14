using UnityEngine;

public class TestObject : InteractableObject
{
    public override void Interact()
    {
        Debug.Log("상호작용");
    }

    public override bool IsInteractionPossible()
    {
        return true;
    }
}
