using System;
using TMPro;
using UnityEngine;


/// <summary>
/// 옷장 자물쇠
/// </summary>
public class DialLock : CloseUpInteractableObject
{
    public Char[] codeList;
    public Char[] currentCode;
    [HideInInspector] public float codeAngleStep;

    private Transform _cylindersTR;
    private DialLockChild[] _dials;

    //임시
    private TextMeshProUGUI text;

    public override void Start()
    {
        base.Start();

        _cylindersTR = transform.Find("Cylinders");
        if (transform.GetChild(1) == null)
            throw new ArgumentException("Cylinders가 없습니다.");

        _dials = _cylindersTR.GetComponentsInChildren<DialLockChild>();
        currentCode = new Char[_dials.Length];

        foreach (DialLockChild dial in _dials)
            dial.parentDialLock = this;

        codeAngleStep = 360f / codeList.Length;
    }

    public override void Interact()
    {
        base.Interact();

        //모든 다이얼 활성화
        foreach (Transform child in _cylindersTR)
        {
            child.GetComponent<ClickHandler>().enabled = true;
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
            child.GetComponent<ClickHandler>().enabled = false;
            child.GetComponent<MeshCollider>().enabled = false;
        }

        foreach (var lockCode in _dials)
        {
            lockCode.OtherPointClick();
        }

        GameManager.Instance.uINavigation.Close();
    }

    public void UpdateCode()
    {
        for (int i = 0; i < _dials.Length; i++)
            currentCode[i] = codeList[_dials[i].CodeIndex];

        text.text = string.Join(" ", currentCode);
    }

    public override bool IsInteractionPossible()
    {
        return true;
    }
}