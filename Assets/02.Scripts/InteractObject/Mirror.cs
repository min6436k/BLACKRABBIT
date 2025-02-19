using System;
using UnityEngine;

public class Mirror : InteractableObject
{
    public override void Interact()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        GetComponentInChildren<ParticleSystem>().Emit(60);
    }


    public override bool IsInteractionPossible()
    {
        return true;
    }
}
