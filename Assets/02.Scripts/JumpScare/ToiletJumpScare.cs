using UnityEngine;

public class ToiletJumpScare : JumpScare
{
    void Start()
    {
        GameManager.Instance.eventManager.ToiletEvent.AddListener(Play); // 이벤트 등록
    }

    protected override void Play()
    {
        // 일단 임시로 디버그 로그만 띄움
        Debug.Log("점프스케어"); 
    }
}
