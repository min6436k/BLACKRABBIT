using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

// 이벤트를 관리할 메니저
public class EventManager : MonoBehaviour
{
    public UnityEvent ToiletDoorOpenEvent; // 화장실 문 열릴때 점프스케어 이벤트
    public UnityEvent SmallRoomDoorOpenEvent;
    public UnityEvent ClosetUnlockEvent;
}
