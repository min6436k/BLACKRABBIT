using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public enum DialState{
    Idle,
    CloseUp,
}

public class DialLockChild : MonoBehaviour, IClickListener
{
    [HideInInspector] public DialLock parentDialLock;
    public int CodeIndex { get; private set; }

    private Tweener _sizeUpTween;
    private Tween _dialMoveTween;
    private Vector3 _originScale;
    private DialState _dialState;

    public void Start()
    {
        InitClickHandler();
        
        _originScale = transform.localScale;
        _sizeUpTween = transform.DOScale(_originScale*1.5f, 0.7f).Pause()
            .SetAutoKill(false).SetEase(Ease.InOutQuart);

        CodeIndex = Random.Range(0, parentDialLock.codeList.Length - 1);
        transform.localEulerAngles = Vector3.up * (CodeIndex * parentDialLock.codeAngleStep);
    }

    public void InitClickHandler()
    {
        ClickHandler clickHandler = gameObject.AddComponent<ClickHandler>();
        clickHandler.Listener = this;
        clickHandler.enabled = false;
    }

    public void GetClickDown()
    {
        if (_dialState == DialState.Idle)
        {
            _dialState = DialState.CloseUp;
            _sizeUpTween.PlayForward(); 
        }
    }



    public void MheelMove(float value)
    {
        if (value != 0 && _dialState == DialState.CloseUp)
        {
            CodeIndex += (int)(value*10);

            Vector3 rotateVector;

            rotateVector = Vector3.up * (CodeIndex * parentDialLock.codeAngleStep); 
            _dialMoveTween.Kill();
            _dialMoveTween = transform.DOLocalRotate(rotateVector, 0.2f);

            if (CodeIndex >= parentDialLock.codeList.Length) CodeIndex = 0;
            else if (CodeIndex < 0) CodeIndex = parentDialLock.codeList.Length - 1;
            parentDialLock.UpdateCode();
        }
    }
    
    public void GetClickUp()
    {
    }
    
    public void GetClick()
    {
    }

    public void OtherPointClick()
    {
        _dialState = DialState.Idle; 
        _sizeUpTween.PlayBackwards();
    }

}
