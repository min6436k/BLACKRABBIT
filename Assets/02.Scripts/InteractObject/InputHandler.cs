using System;
using UnityEngine;


/// <summary>
/// 모든 자물쇠에 필요한 마우스 Input 구현
/// </summary>
public interface IInputListener
{
    void InitClickHandler();

    void OnClick()
    {
    }

    void OtherPointClick()
    {
    }

    void InputUpdate()
    {
    }
}
public class InputHandler : MonoBehaviour
{

    [HideInInspector] public IInputListener Listener;
    
    private LayerMask _ignoreLayer;

    private void Start()
    {
        _ignoreLayer = ~(1 << LayerMask.NameToLayer("Player"));
    }

    void Update()
    {
        Ray ray = GameManager.Instance.cameraManager.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit,  float.MaxValue, _ignoreLayer) && hit.transform == gameObject.transform)
            {
                Listener.OnClick();
            }
            else
            {
                Listener.OtherPointClick();
            }
        }

        Listener.InputUpdate();
    }
}
