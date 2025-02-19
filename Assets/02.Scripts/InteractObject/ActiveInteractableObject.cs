using DG.Tweening;
using UnityEngine;

public interface ActiveInteractable
{
    Tweener SetTween();
}

public abstract class ActiveInteractableObject : InteractableObject, ActiveInteractable
{
    protected Tweener ActiveTween;
    protected bool IsActive = false;

    public virtual void Start()
    {
        ActiveTween = SetTween().SetAutoKill(false).Pause();
    }
    
    public abstract Tweener SetTween();
    
    public override void Interact()
    {
        if(IsActive == false) ActiveTween.PlayForward();
        else ActiveTween.PlayBackwards();
        IsActive = !IsActive;
    }
}
