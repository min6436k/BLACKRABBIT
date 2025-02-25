using DG.Tweening;
using UnityEngine;

public class Closet : ActiveInteractableObject, ILockedObject<DialLock>
{
    [field : SerializeField]
    public DialLock Lock { get; set; }

    private bool _isjumpscareDone = false;

    public override void Interact()
    {
        base.Interact();
        if (_isjumpscareDone == false)
        {
            GameManager.Instance.eventManager.ClosetUnlockEvent.Invoke();
            _isjumpscareDone = true;
        }
        GameManager.Instance.soundManager.PlaySFX("옷장 사운드");

    }

    public override Tweener SetTween()
    {
        return transform.DOLocalRotate(new Vector3(0, 0, 90), 2.5f).Pause()
            .SetAutoKill(false).SetEase(Ease.InOutCubic);
    }

    public override void InteractFailed()
    {
        GameManager.Instance.soundManager.PlaySFX("옷장 잠겨있을때 사운드");
    }

    public override bool IsInteractionPossible()
    {
        return !Lock.isLock;
    }
}
