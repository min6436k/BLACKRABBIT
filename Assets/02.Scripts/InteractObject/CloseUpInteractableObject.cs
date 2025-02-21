using System;
using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;


/// <summary>
/// 상호작용 시 클로즈업 효과가 들어가는 오브젝트에서 상속하는 클래스
/// </summary>
public abstract class CloseUpInteractableObject : InteractableObject
{
    private Transform _closUpTargetTR;
    private Sequence _closeUp;

    public virtual void Start()
    {
        _closUpTargetTR = transform.Find("CloseUpPosition");
        
        if (_closUpTargetTR == null)
            throw new System.Exception("CloseUpPosition이 없거나 첫 번째 자식 오브젝트가 아닙니다.");
        
        _closeUp = DOTween.Sequence().Pause()
            .SetAutoKill(false).SetEase(Ease.InOutQuart);
        _closeUp.Append(transform.DORotate(_closUpTargetTR.eulerAngles, 1f));
        
        _closeUp.Join(transform.DOMove(_closUpTargetTR.position, 1));
    }
    
    public override void Interact()
    {
        GameManager.Instance.playerController.CurrentState = PlayerState.Interact;

        DOVirtual.DelayedCall(0.2f, () => _closeUp.PlayForward());

        Cursor.lockState = CursorLockMode.None;
        
        ChangeLayer(transform, "OverUI");

    }
    
    public virtual void OutInteract()
    {
        _closeUp.PlayBackwards();
        
        Cursor.lockState = CursorLockMode.Locked;
        
        ChangeLayer(transform, "Interactables");
    }

    public virtual void ChangeLayer(Transform trans, string layerName)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in trans)
            ChangeLayer(child, layerName);
    }
}
