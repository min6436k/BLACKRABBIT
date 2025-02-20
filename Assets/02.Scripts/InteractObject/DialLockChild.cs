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

public class DialLockChild : MonoBehaviour, IInputListener
{
    public int CodeIndex { get; private set; }

    private DialLock _parentDialLock;
    private Tweener _sizeUpTween;
    private Tween _dialMoveTween;
    private Vector3 _originScale;
    private DialState _dialState;

    public void Init(DialLock parentDialLock)
    {
        InitClickHandler();

        _parentDialLock = parentDialLock;
        _originScale = transform.localScale;
        _sizeUpTween = transform.DOScale(_originScale*1.5f, 0.7f).Pause()
            .SetAutoKill(false).SetEase(Ease.InOutQuart);

        CodeIndex = Random.Range(0, parentDialLock.codeList.Length - 1);
        transform.localEulerAngles = Vector3.up * (CodeIndex * parentDialLock.codeAngleStep);
    }

    public void InitClickHandler()
    {
        InputHandler inputHandler = gameObject.AddComponent<InputHandler>();
        inputHandler.Listener = this;
        inputHandler.enabled = false;
    }

    public void OnClick()
    {
        if (_dialState == DialState.Idle)
        {
            _dialState = DialState.CloseUp;
            _sizeUpTween.PlayForward(); 
        }
    }



    public void InputUpdate()
    {
        float value = Input.GetAxisRaw("Mouse ScrollWheel");

        if (value != 0 && _dialState == DialState.CloseUp)
        {
            GameManager.Instance.soundManager.PlaySFX("자물쇠 다이얼 사운드",volume:0.4f);
            CodeIndex += (int)(value*10);

            Vector3 rotateVector;

            rotateVector = Vector3.up * (CodeIndex * _parentDialLock.codeAngleStep); 
            _dialMoveTween.Kill();
            _dialMoveTween = transform.DOLocalRotate(rotateVector, 0.2f);

            if (CodeIndex >= _parentDialLock.codeList.Length) CodeIndex = 0;
            else if (CodeIndex < 0) CodeIndex = _parentDialLock.codeList.Length - 1;
            _parentDialLock.UpdateCode();
        }
    }

    public void OtherPointClick()
    {
        _dialState = DialState.Idle; 
        _sizeUpTween.PlayBackwards();
    }

}
