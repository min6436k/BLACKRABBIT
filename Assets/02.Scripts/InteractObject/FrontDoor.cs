using DG.Tweening;
using UnityEngine;

public class FrontDoor : ActiveInteractableObject, ILockedObject<ArrowLock>
{
    [field : SerializeField]
    public ArrowLock Lock { get; set; }

    public override Tweener SetTween()
    {
        return transform.DOLocalRotate(new Vector3(0, 0, 90), 2.5f).Pause()
            .SetAutoKill(false).SetEase(Ease.InOutCubic);
    }

    public override void InteractFailed()
    {
        GameManager.Instance.soundManager.PlaySFX("잠긴 문 사운드",volume:15f,pitch:1.15f);
    }

    public override bool IsInteractionPossible()
    {
        return !Lock.isLock;
    }
}