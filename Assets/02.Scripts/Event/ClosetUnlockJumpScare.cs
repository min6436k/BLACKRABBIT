using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

public class ClosetUnlockJumpScare : JumpScare
{
    private void Start()
    {
         GameManager.Instance.eventManager.ClosetUnlockEvent.AddListener(Play);
    }

    [ContextMenu("START")]
    protected override void Play()
    {
        GameManager.Instance.playerController.JumpScareTimeLine(0);
        
        Debug.Log("옷장 자물쇠 열림 점프스케어");
    }
}
