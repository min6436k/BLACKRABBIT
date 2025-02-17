using System;
using UnityEngine;


/// <summary>
/// 모든 자물쇠에 필요한 마우스 Input 구현
/// </summary>
public interface IClickListener
{
    void InitClickHandler();
    void GetClickDown();
    
    void GetClick();
    void GetClickUp();
    void OtherPointClick();
    void MheelMove(float value);
}
public class ClickHandler : MonoBehaviour
{

    [HideInInspector] public IClickListener Listener;
    void Update()
    {
        Ray ray = GameManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.transform == gameObject.transform)
            {
                Listener.GetClickDown();
            }
            else
            {
                Listener.OtherPointClick();
            }
            
        }else if (Input.GetMouseButton(0))
            Listener.GetClick();
        else if(Input.GetMouseButtonUp(0))
            Listener.GetClickUp();

        Listener.MheelMove(Input.GetAxisRaw("Mouse ScrollWheel"));
    }
}
