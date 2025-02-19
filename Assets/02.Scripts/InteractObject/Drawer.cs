using System;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;

public class Drawer : ActiveInteractableObject
{

    private GameObject _note;
    public override void Start()
    {
        base.Start();

        _note = transform.Find("Note").gameObject;
        if (_note == null)
            throw new System.Exception("서랍 안에 Note가 없습니다.");

        _note.layer = LayerMask.NameToLayer("Default");
    }

    public override Tweener SetTween()
    {
        return transform.DOLocalMove(transform.position + Vector3.right * 0.3f, 1.5f).SetAutoKill(false).Pause().SetEase(Ease.InOutSine);
    }

    public override void Interact()
    {
        base.Interact();
        _note.layer = IsActive switch
        {
            true => LayerMask.NameToLayer("Interactables"),
            false => LayerMask.NameToLayer("Default")
        };
    }

    public override bool IsInteractionPossible()
    {
        return true;
    }
}
