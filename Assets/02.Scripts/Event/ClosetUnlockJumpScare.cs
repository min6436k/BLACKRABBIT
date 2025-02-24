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
        GameManager.Instance.playerController.CurrentState = PlayerState.JumpScare;
        GameManager.Instance.playerController.gameObject.GetComponent<PlayableDirector>().Play();
        DOVirtual.DelayedCall(4.7f, () => GameManager.Instance.soundManager.PlaySFX("1번 점프스케어 사운드", pitch: 1.2f));
        DOVirtual.DelayedCall(7.5f, () => SceneLoadWithFade.Instance.FadeOut());
        DOVirtual.DelayedCall(12.5f, () =>
        {
            SceneLoadWithFade.Instance.FadeIn();
            GameManager.Instance.playerController.CurrentState = PlayerState.Idle;
        });
        
        Debug.Log("옷장 자물쇠 열림 점프스케어");
    }
}
