using System;
using UnityEngine;

/// <summary>
/// 상호작용 인터페이스
/// </summary>
public interface IInteractable
{
    // 상호작용 시 행동
    void Interact();
    
    // 상호작용이 가능한지 확인
    bool IsInteractionPossible();
}

/// <summary>
/// 상호작용 가능한 오브젝트의 추상 클래스
/// </summary>
public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    private readonly Color _activeColor = ColorUtility.TryParseHtmlString("#9DFE44", out var color) ? color : Color.white;
    private readonly Color _inactiveColor = ColorUtility.TryParseHtmlString("#ADADAD", out var inactiveColor) ? inactiveColor : Color.white;

    
    private Outline _outline;

    private void Awake()
    {
        //아웃라인 에셋 초기설정
        _outline = gameObject.AddComponent<Outline>();
        _outline.enabled = false;
        _outline.OutlineColor = _activeColor;
        _outline.OutlineWidth = 6.5f;
    }

    public void SetOutLine(bool isActive)
    {
        _outline.OutlineColor = IsInteractionPossible() ? _activeColor : _inactiveColor;
        _outline.enabled = isActive;
    }
    public abstract void Interact();
    public abstract bool IsInteractionPossible(); 
}
