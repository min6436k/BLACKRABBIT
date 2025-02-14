using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, interactionDistance) &&
            Input.GetKeyDown(KeyCode.F))
        {
            if (hit.collider.gameObject.TryGetComponent(out InteractableObject targetObject))
            {
                if (targetObject.IsInteractionPossible())
                {
                    targetObject.Interact();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_cam == null)
            return;
        Gizmos.DrawRay(_cam.transform.position, _cam.transform.forward * interactionDistance);
    }
}
