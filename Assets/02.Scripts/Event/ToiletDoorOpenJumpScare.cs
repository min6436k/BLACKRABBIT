using UnityEngine;

public class ToiletDoorOpenJumpScare : JumpScare
{
    void Start()
    {
        GameManager.Instance.eventManager.ToiletDoorOpenEvent.AddListener(Play); // 이벤트 등록
    }

    protected override void Play()
    {
        // 일단 임시로 디버그 로그만 띄움
        Debug.Log("화장실 문 열림 점프스케어"); 
    }
}
