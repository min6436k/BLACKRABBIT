using System;
using UnityEngine;

public class TV : InteractableObject
{
    private GameObject TVScreen;

    private void Start()
    {
        TVScreen = transform.GetChild(0).gameObject;
    }

    public override void Interact()
    {
        TVScreen.SetActive(!TVScreen.activeSelf);
    }


    public override bool IsInteractionPossible()
    {
        return true;
    }
}
