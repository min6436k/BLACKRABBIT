using UnityEngine;

public class Washbasin : CloseUpInteractableObject
{
    public Mirror mirror;
    public GameObject corpse;

    public override void Interact()
    {
        base.Interact();
        corpse.SetActive(false);
        mirror.WashbasinInteract();
        GameManager.Instance.gameFlags.isCorpseDisable = true;
        GameManager.Instance.cameraManager.ViewChange(GameManager.Instance.cameraManager.washbasinCam);
    }

    public override bool IsInteractionPossible()
    {
        return GameManager.Instance.gameFlags.isMirrorBroken;
    }
    
    public override void ChangeLayer(Transform trans, string layerName)
    {
    }
}
