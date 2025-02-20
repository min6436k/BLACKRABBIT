using System;
using UnityEngine;

public class ClosetUnlockJumpScare : JumpScare
{
    private void Start()
    {
         GameManager.Instance.eventManager.ClosetUnlockEvent.AddListener(Play);
    }

    protected override void Play()
    {
        Debug.Log("옷장 자물쇠 열림 점프스케어");
    }
}
