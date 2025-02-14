using UnityEngine;

public class TestObject : InteractableObject
{
    public override void Interact()
    {
        Debug.Log(GetComponent<Outline>().OutlineColor);
    }

    public override bool IsInteractionPossible()
    {
        return true;
    }
}
