using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private float interactionRadius;

    private InteractableObject _lastDetectedObject = null;

    private PlayerController PlayerController => GameManager.Instance.playerController;

    private Camera Cam => GameManager.Instance.cameraManager.mainCamera;


    private void Update()
    {
        SearchObj();

        if (Input.GetKeyDown(KeyCode.Escape))
            ESCInput();
    }

    private void SearchObj()
    {
        if (Physics.SphereCast(Cam.transform.position, interactionRadius, Cam.transform.forward, out RaycastHit hit,
                interactionDistance, LayerMask.GetMask("Interactables"))
            && PlayerController.CurrentState == PlayerState.Idle)
        {
            //마지막으로 감지한 오브젝트와 다를 시 이전 오브젝트 아웃라인 해제
            if (_lastDetectedObject != null
                && hit.collider.gameObject != _lastDetectedObject.gameObject)
            {
                _lastDetectedObject.SetOutLine(false);
            }

            if (hit.collider.gameObject.TryGetComponent(out InteractableObject targetObject))
            {
                if (_lastDetectedObject != targetObject) _lastDetectedObject = targetObject;

                targetObject.SetOutLine(true);

                if (Input.GetKeyDown(Setting.CurrentKeyValues[EKeyInputs.Interact]))
                {
                    if (targetObject.IsInteractionPossible())
                        targetObject.Interact();
                    else targetObject.InteractFailed();
                }
            }
        }
        else
        {
            //레이캐스트가 감지되지 않을 시 아웃라인 해제
            if (_lastDetectedObject != null)
            {
                _lastDetectedObject.SetOutLine(false);
            }
        }
    }

    /// <summary>
    /// 클로즈업과 같은 상호작용 상태에서 벗어나는 함수
    /// </summary>
    public void ESCInput()
    {
        switch (PlayerController.CurrentState)
        {
            case PlayerState.Interact:
                PlayerController.CurrentState = PlayerState.Idle;
                GameManager.Instance.cameraManager.PlayerView();

                GameManager.Instance.uINavigation.Close();
                if (_lastDetectedObject is CloseUpInteractableObject closeUpObject)
                    closeUpObject.OutInteract();
                break;

            case PlayerState.Option:
                GameManager.Instance.uINavigation.Close();
                Cursor.lockState = CursorLockMode.Locked;
                GameManager.Instance.playerController.CurrentState = PlayerState.Idle;
                break;
            case PlayerState.Idle:
                GameManager.Instance.uINavigation.Open("Option");
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Cam == null)
            return;
        Gizmos.DrawWireSphere(Cam.transform.position + (Cam.transform.forward * interactionDistance),
            interactionRadius);
    }
}