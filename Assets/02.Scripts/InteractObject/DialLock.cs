using System;
using DG.Tweening;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


public enum DialLockType
{
    Closet,
    Toliet,
}
public class DialLock : CloseUpInteractableObject
{
    public DialLockType dialLockType;
    public Char[] codeList;
    private Char[] _currentCode;
    [HideInInspector] public float codeAngleStep;

    private Transform _cylindersTR;
    private DialLockChild[] _dials;

    public GameObject[] chainAndLock;

    //임시
    private TextMeshProUGUI text;
    
    [SerializeField] private Char[] _passward;
    public bool isLock;

    public override void Start()
    {
        base.Start();
        
        isLock = true;

        _cylindersTR = transform.Find("Cylinders");
        if (_cylindersTR == null)
            throw new System.Exception("Cylinders가 없습니다.");

        _dials = _cylindersTR.GetComponentsInChildren<DialLockChild>();
        _currentCode = new Char[_dials.Length];

        foreach (DialLockChild dial in _dials)
            dial.Init(this);

        codeAngleStep = 360f / codeList.Length;
    }

    public override void Interact()
    {
        base.Interact();

        CinemachineCamera tempCam = dialLockType switch
        {
            DialLockType.Closet => GameManager.Instance.cameraManager.closetLockCam,
            DialLockType.Toliet => GameManager.Instance.cameraManager.toiletCam,
            _ => GameManager.Instance.cameraManager.playerCam
        };
        GameManager.Instance.cameraManager.ViewChange(tempCam);


        //모든 다이얼 활성화
        foreach (Transform child in _cylindersTR)
        {
            child.GetComponent<InputHandler>().enabled = true;
            child.GetComponentInChildren<MeshCollider>().enabled = true;
        }

        text = GameManager.Instance.uINavigation.Open("InteractCloseUpUI").GetComponentInChildren<TextMeshProUGUI>();
        UpdateCode();
    }

    public override void OutInteract()
    {
        base.OutInteract();

        //모든 다이얼 비활성화
        foreach (Transform child in _cylindersTR)
        {
            child.GetComponent<InputHandler>().enabled = false;
            child.GetComponent<MeshCollider>().enabled = false;
        }

        foreach (var lockCode in _dials)
        {
            lockCode.OtherPointClick();
        }

        GameManager.Instance.uINavigation.Close();
    }
    
    public void CheckCode()
    {
        if (dialLockType == DialLockType.Closet && GameManager.Instance.gameFlags.isClosetUnlocked) // 입력이 중복으로 들어오므로 점프스케어가 여러번 실행되는걸 방지
            return;
        
        for (int i = 0; i < _dials.Length; i++)
        {
            if (_currentCode[i] != _passward[i])
            {
                GameManager.Instance.soundManager.PlaySFX("자물쇠 실패 사운드",volume:3f);

                return;
            }
        }

        DOVirtual.DelayedCall(1, () =>
        {
            GameManager.Instance.soundManager.PlaySFX("자물쇠 성공 사운드",volume:2.5f);

            foreach (GameObject o in chainAndLock)
            {
                o.GetComponent<MeshCollider>().enabled = false;
                o.GetComponent<MeshRenderer>().enabled = false;
            }
        });
        GameManager.Instance.playerController.CurrentState = PlayerState.Idle;
        GameManager.Instance.cameraManager.PlayerView();
        OutInteract();
        SceneLoadWithFade.Instance.FadeOutIn();
        
        if (dialLockType == DialLockType.Closet)
        {
            GameManager.Instance.gameFlags.isClosetUnlocked = true;
        }
        isLock = false;
    }

    public void UpdateCode()
    {
        for (int i = 0; i < _dials.Length; i++)
            _currentCode[i] = codeList[_dials[i].CodeIndex];

        text.text = string.Join("", _currentCode);
    }

    public override bool IsInteractionPossible()
    {
        return isLock;
    }
}