using UnityEngine;

// 점프 스케어 발동용 화장실 문
public class ToiletDoor : Door
{
    private bool wasOpened;
    
    public override void Interact()
    {
        base.Interact();
        if (wasOpened == false)
        {
            GameManager.Instance.eventManager.ToiletEvent.Invoke();
            wasOpened = true;
        }
    }
}
