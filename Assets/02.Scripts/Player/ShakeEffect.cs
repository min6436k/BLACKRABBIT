using DG.Tweening;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public Transform[] shakeRotObj;
    public Transform[] shakePosObj;

    private void ShakeRot(float duration,float strength,int vibrato)
    {
        foreach (var o in shakeRotObj)
        {
            o.DOShakeRotation(duration, strength, vibrato, 100, false,ShakeRandomnessMode.Harmonic).SetUpdate(UpdateType.Late);
        }
    } 
    
    private void ShakePos(float duration,float strength,int vibrato)
    {
        foreach (var o in shakePosObj)
        {
            o.DOShakePosition(duration, strength,vibrato,10,false,false,ShakeRandomnessMode.Harmonic);
        }
    }

    public void Scare2Shake()
    {
        ShakeRot(4, 1, 20);
        ShakePos(4, 0.05f, 30);
    }

    public void FadeIn()
    {
        SceneLoadWithFade.Instance.FadeIn();
    }
    public void FadeOut()
    {
        GameManager.Instance.playerController.CurrentState = PlayerState.Idle;

        SceneLoadWithFade.Instance.FadeOut();
    }
}
