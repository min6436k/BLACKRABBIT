using System;
using UnityEngine;

public class Mirror : InteractableObject
{
    public override void Interact()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        GetComponentInChildren<ParticleSystem>().Emit(100);
        GetComponent<AudioSource>().Play();
    }


    public override bool IsInteractionPossible()
    {
        return true;
    }
}
