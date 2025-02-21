using UnityEngine;

/// <summary>
/// 자물쇠가 걸려있는 물체의 인터페이스
/// </summary>
public interface ILockedObject<TLockType> where TLockType : CloseUpInteractableObject
{
    public TLockType Lock { get; set; } // 잠그고 있는 자물쇠
}