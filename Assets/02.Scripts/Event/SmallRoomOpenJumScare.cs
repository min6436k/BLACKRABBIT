using UnityEngine;

public class SmallRoomOpenJumScare : JumpScare
{
    private void Start()
    {
        GameManager.Instance.eventManager.SmallRoomDoorOpenEvent.AddListener(Play);
    }

    protected override void Play()
    {
        Debug.Log("작은 방 문 열림 점프 스케어");
    }
}
