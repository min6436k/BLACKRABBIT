using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ArrowLock : CloseUpInteractableObject, IInputListener
{
    public KeyCode[] codeList;
    private KeyCode[] _currentCode = new KeyCode[4];
    private int _currentIndex = 0;

    public Transform _handleTR;
    private Vector3 _handleOriginPosition;
    private Tweener _arrowMoveTween;
    private bool _isMoving = false; 

    //임시
    private TextMeshProUGUI text;

    public override void Start()
    {
        base.Start();
        InitClickHandler();
        
        _handleTR = transform.Find("Handle");
        if (_handleTR == null)
            throw new ArgumentException("Handle이 없습니다.");

    }
    
    public void InitClickHandler()
    {
        InputHandler inputHandler = gameObject.AddComponent<InputHandler>();
        inputHandler.Listener = this;
        inputHandler.enabled = false;
    }

    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.cameraManager.ViewChange(GameManager.Instance.cameraManager.exitDoorLockCam);
        
        text = GameManager.Instance.uINavigation.Open("InteractCloseUpUI").GetComponentInChildren<TextMeshProUGUI>();

        GetComponent<InputHandler>().enabled = true;
    }


    public override void OutInteract()
    {
        base.OutInteract();
        GameManager.Instance.uINavigation.Close();
        GetComponent<InputHandler>().enabled = false;
    }
    
    public void InputUpdate()
    {
        if (_isMoving) return;
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ArrowMove(KeyCode.UpArrow).Play();
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            ArrowMove(KeyCode.DownArrow).Play();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            ArrowMove(KeyCode.RightArrow).Play();
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            ArrowMove(KeyCode.LeftArrow).Play();
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            _currentIndex = 0;
            _currentCode = new KeyCode[4];
            text.text = "";
        }
    }

    private Tweener ArrowMove(KeyCode inputArrow)
    {
        if (_currentIndex < 4)
        {
            _currentCode[_currentIndex++] = inputArrow;
            
            char temp  = inputArrow switch
            {
                KeyCode.UpArrow => '↑',
                KeyCode.DownArrow => '↓',
                KeyCode.RightArrow => '→',
                KeyCode.LeftArrow => '←',
                _ => '\0'
            };

            text.text += temp;
        }
        
        _handleOriginPosition = _handleTR.transform.position;

        Vector3 moveDir = Vector3.zero;
        
        moveDir = inputArrow switch
        {
            KeyCode.UpArrow    => Vector3.up,
            KeyCode.DownArrow  => Vector3.down,
            KeyCode.RightArrow => Vector3.forward,
            KeyCode.LeftArrow  => Vector3.back,
            _ => Vector3.zero
        };

        
        _isMoving = true;
        
        _arrowMoveTween = _handleTR.transform.DOMove(_handleOriginPosition + moveDir/90, 0.22f).SetEase(Ease.OutQuart)
            .OnComplete(() => _handleTR.transform.DOMove(_handleOriginPosition, 0.25f).SetEase(Ease.OutElastic,0.5f)
                .OnComplete(()=> _isMoving = false));
        return _arrowMoveTween;
    }

    public override bool IsInteractionPossible()
    {
        return true;
    }
}
