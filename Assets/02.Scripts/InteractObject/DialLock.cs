using System;
using TMPro;
using Unity.Cinemachine;
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
            child.GetComponent<MeshCollider>().enabled = true;
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
        for (int i = 0; i < _dials.Length; i++)
        {
            if (_currentCode[i] != _passward[i])
            {
                Debug.Log("비밀번호 틀림");
                return;
            }
        }

        Debug.Log("잠금 해제");
        if (dialLockType == DialLockType.Closet)
        {
            GameManager.Instance.gameFlags.isUnlockCloset = true;
            GameManager.Instance.eventManager.ClosetUnlockEvent.Invoke();
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