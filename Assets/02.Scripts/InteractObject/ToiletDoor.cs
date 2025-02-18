using UnityEngine;

// 점프 스케어 발동용 화장실 문
public class ToiletDoor : Door
{
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
}
