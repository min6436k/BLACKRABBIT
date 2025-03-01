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
    [SerializeField] private KeyCode[] _password;

    public Transform _handleTR;

    public GameObject[] chainAndLock;
    
    private Vector3 _handleOriginPosition;
    private Tweener _arrowMoveTween;
    private bool _isMoving = false;

    public bool isLock;

    //임시
    public TextMeshProUGUI[] text;

    public override void Start()
    {
        base.Start();
        isLock = true;
        InitClickHandler();
        
        _handleTR = transform.Find("Handle");
        if (_handleTR == null)
            throw new System.Exception("Handle이 없습니다.");

    }
    
    public void InitClickHandler()
    {
        InputHandler inputHandler = gameObject.AddComponent<InputHandler>();
        inputHandler.Listener = this;
        inputHandler.enabled = false;
    }

    public override void Interact()
    {
        if (GameManager.Instance.gameFlags.isCorpseDisable)
        {
            GameManager.Instance.playerController.JumpScareTimeLine(1);
            GameManager.Instance.gameFlags.isCorpseDisable = false;
        }
        else
        {
            base.Interact();
            GameManager.Instance.cameraManager.ViewChange(GameManager.Instance.cameraManager.exitDoorLockCam);
        
            text = GameManager.Instance.uINavigation.Open("ArrowLockUI").GetComponentsInChildren<TextMeshProUGUI>();

            GetComponent<InputHandler>().enabled = true;    
        }
    }


    public override void OutInteract()
    {
        base.OutInteract();
        GameManager.Instance.uINavigation.Close();
        GetComponent<InputHandler>().enabled = false;
        CodeInit();
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
            CodeInit();
        }
    }

    private void CodeInit()
    {
        if (!isLock)
            return;
        
        if (_currentIndex == codeList.Length)
        {
            bool flag = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (_currentCode[i] != _password[i])
                    flag = true;
            }

            if (!flag)
            {
                isLock = false;
                
                GameManager.Instance.playerController.CurrentState = PlayerState.Idle;
                GameManager.Instance.cameraManager.PlayerView();
                OutInteract();
                SceneLoadWithFade.Instance.FadeOutIn();
                DOVirtual.DelayedCall(1, () =>
                {
                    GameManager.Instance.soundManager.PlaySFX("자물쇠 성공 사운드",volume:2.5f);

                    foreach (GameObject o in chainAndLock)
                    {
                        o.GetComponent<MeshRenderer>().enabled = false;
                        o.GetComponent<MeshCollider>().enabled = false;
                    }
                });
            }
            else
            {
                GameManager.Instance.soundManager.PlaySFX("자물쇠 실패 사운드",volume:3f);

            }
        }
        
        _currentIndex = 0;
        _currentCode = new KeyCode[4];
        foreach (TextMeshProUGUI textMeshProUGUI in text)
            textMeshProUGUI.text = "";
    }

    private Tweener ArrowMove(KeyCode inputArrow)
    {
        GameManager.Instance.soundManager.PlaySFX("자물쇠 다이얼 사운드",volume:0.4f);

        if (_currentIndex < 4)
        {
            char temp  = inputArrow switch
            {
                KeyCode.UpArrow => '↑',
                KeyCode.DownArrow => '↓',
                KeyCode.RightArrow => '→',
                KeyCode.LeftArrow => '←',
                _ => '\0'
            };
            text[_currentIndex].text = temp.ToString();
            _currentCode[_currentIndex++] = inputArrow;
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
        return isLock;
    }
}
