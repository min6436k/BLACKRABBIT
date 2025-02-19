using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Door : ActiveInteractableObject
{
    public override Tweener SetTween()
    {
        return transform.DORotate(new Vector3(0,-180,-180), 1).Pause()
            .SetAutoKill(false).SetEase(Ease.InOutQuart);
    }

    public override bool IsInteractionPossible()
    {
        return true;
    }
}
