using System;
using UnityEngine;

public class Mirror : InteractableObject
{
    public override void Interact()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        GetComponentInChildren<ParticleSystem>().Emit(100);
        GameManager.Instance.soundManager.PlaySFX("거울 깨는 사운드");
        GameManager.Instance.gameFlags.isMirrorBroken = true;
    }


    public override bool IsInteractionPossible()
    {
        return true;
    }
}
