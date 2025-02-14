using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    
    private InteractableObject _lastDetectedObject = null;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, interactionDistance))
        {
            if (_lastDetectedObject != null
            && hit.collider.gameObject != _lastDetectedObject.gameObject)
            {
                _lastDetectedObject.SetOutLine(false);    
            }
            
            if (hit.collider.gameObject.TryGetComponent(out InteractableObject targetObject))
            {
                if (_lastDetectedObject != targetObject) _lastDetectedObject = targetObject;
                
                targetObject.SetOutLine(true);    
                
                if (Input.GetKeyDown(KeyCode.F) && targetObject.IsInteractionPossible())
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
