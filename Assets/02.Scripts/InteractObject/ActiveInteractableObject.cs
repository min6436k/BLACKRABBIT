using System.Collections;
using DG.Tweening;
using UnityEngine;

public interface ActiveInteractable
{
    Tweener SetTween();
}

public abstract class ActiveInteractableObject : InteractableObject, ActiveInteractable
{
    public float audioTransitionTime;
    
    protected Tweener ActiveTween;
    protected bool IsActive = false;

    private bool _isPlaying = false;
    private AudioSource _activeAudio;
    public virtual void Start()
    {
        ActiveTween = SetTween().SetAutoKill(false).Pause().OnPlay(()=>_isPlaying = true).OnComplete(()=> _isPlaying = false).OnRewind(()=> _isPlaying = false);
        _activeAudio = GetComponent<AudioSource>();
    }
    
    public abstract Tweener SetTween();
    
    public override void Interact()
    {
        if(_isPlaying) return;
        if (IsActive == false)
        {
            _activeAudio.time = 0;
            _activeAudio.Play();
            StartCoroutine(TransitionTime());
            ActiveTween.PlayForward();
        }
        else
        {
            StopAllCoroutines();
            _activeAudio.Stop();
            _activeAudio.time = audioTransitionTime;
            _activeAudio.Play();
            ActiveTween.PlayBackwards();
        }
        IsActive = !IsActive;
    }

    IEnumerator TransitionTime()
    {
        yield return new WaitForSeconds(audioTransitionTime);
        _activeAudio.Stop();
    }
}
