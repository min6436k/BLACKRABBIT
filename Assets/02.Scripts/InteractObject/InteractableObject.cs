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
    public abstract void Interact();
    public abstract bool IsInteractionPossible(); 
}
