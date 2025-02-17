using UnityEngine;

public class ToiletJumpScare : JumpScare
{
    void Start()
    {
        GameManager.Instance.eventManager.ToiletEvent.AddListener(Play);
    }

    protected override void Play()
    {
        Debug.Log("점프스케어");
    }
}
