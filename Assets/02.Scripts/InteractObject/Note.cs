using UnityEngine;

public class Note : CloseUpInteractableObject, IInputListener
{
    private InputHandler _inputHandler;
    public override void Start()
    {
        base.Start();
        InitClickHandler();
    }

    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.cameraManager.ViewChange(GameManager.Instance.cameraManager.noteCam);
        _inputHandler.enabled = true;    

    }

    public override void OutInteract()
    {
        base.OutInteract();
        _inputHandler.enabled = false;    

    }

    public void InitClickHandler()
    {
        _inputHandler = gameObject.AddComponent<InputHandler>();
        _inputHandler.Listener = this;
        _inputHandler.enabled = false;
    }
    
    public void OnClick()
    {
        ChangeLayer(transform,"Default");
        _inputHandler.enabled = false;    
        GameManager.Instance.uINavigation.Open("NotePaperUI");
    }

    public override bool IsInteractionPossible()
    {
        return true;
    }
}
