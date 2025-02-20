using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TV : CloseUpInteractableObject
{
    public Texture[] TVImages;
    private GameObject TVScreen;
    public Material TVMat;
    private AudioSource _audioSource;

    private void Start()
    {
        TVScreen = transform.GetChild(0).gameObject;
        TVMat = TVScreen.GetComponent<MeshRenderer>().materials[1];
        _audioSource = GetComponent<AudioSource>();
        TVMat.SetTexture("Image",TVImages[0]);
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(PlayScreen());
        GameManager.Instance.cameraManager.ViewChange(GameManager.Instance.cameraManager.tvCam);
    }


    public override bool IsInteractionPossible()
    {
        return true;
    }

    public override void OutInteract()
    {
        base.OutInteract();
        StartCoroutine(OffEffect());
    }

    IEnumerator PlayScreen()
    {
        yield return StartCoroutine(OnEffect());
        foreach (Texture i in TVImages)
        {
            TVMat.SetTexture("_Image",i);
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator OnEffect()
    {
        _audioSource.Play();
        _audioSource.DOFade(0.2f, 0.4f).SetDelay(0.5f);
        
        TVScreen.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        TVScreen.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        TVScreen.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        TVScreen.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        TVScreen.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        TVScreen.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        TVScreen.SetActive(true);
    }
    
    IEnumerator OffEffect()
    {
        _audioSource.DOFade(0f, 0.4f).OnComplete(_audioSource.Stop);

        yield return new WaitForSeconds(0.05f);
        TVScreen.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        TVScreen.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        TVScreen.SetActive(false);
    }
}
