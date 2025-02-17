using UnityEngine;

// 점프 스케어 발동용 화장실 문
public class ToiletDoor : Door
{
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.eventManager.ToiletEvent.Invoke();
    }
}
