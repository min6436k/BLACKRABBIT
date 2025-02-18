using UnityEngine;

// 점프 스케어 발동용 화장실 문 ILockedObject 인터페이스로 잠금 구현
public class ToiletDoor : Door, ILockedObject<DialLock>
{
    [field : SerializeField] public DialLock Lock { get; set; }
    
    private bool wasOpened; // 열린적 있는지 체크

    public override void Interact()
    {
        base.Interact();
        if (wasOpened == false) // 첫번째 열릴 때만 작동
        {
            GameManager.Instance.eventManager.ToiletEvent.Invoke();
            wasOpened = true;
        }
    }
    
    public override bool IsInteractionPossible()
    {
        return !Lock.isLock; // 자물쇠가 열려있는지
    }
}
