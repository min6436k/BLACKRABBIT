using System;
using UnityEngine;

public class Mirror : InteractableObject
{
    public Material toiletMirror2;
    public Material toiletMirror3;

    private MeshRenderer _meshRenderer;

    private bool _isInteracted = false;
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void Interact()
    {
        _isInteracted = true;
        _meshRenderer.material = toiletMirror2;
        gameObject.layer = LayerMask.NameToLayer("Default");
        GetComponentInChildren<ParticleSystem>().Emit(100);
        GameManager.Instance.soundManager.PlaySFX("거울 깨는 사운드");
        GameManager.Instance.gameFlags.isMirrorBroken = true;
    }

    public void WashbasinInteract()
    {
        if(_isInteracted) _meshRenderer.material = toiletMirror3;
    }


    public override bool IsInteractionPossible()
    {
        return true;
    }
}
