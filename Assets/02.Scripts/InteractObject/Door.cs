using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Door : InteractableObject
{
    private Tweener _openDoor;
    private bool _isDoorOpen = false;

    void Start()
    {
        _openDoor = transform.DORotate(new Vector3(0,20,-180), 1).Pause()
            .SetAutoKill(false).SetEase(Ease.InOutQuart);
    }
    public override void Interact()
    {
        if(_isDoorOpen == false) _openDoor.PlayForward();
        else _openDoor.PlayBackwards();
        _isDoorOpen = !_isDoorOpen;
    }

    public override bool IsInteractionPossible()
    {
        return true;
    }
}
