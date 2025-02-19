using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TV : InteractableObject
{
    private GameObject TVScreen;
    private AudioSource _audioSource;

    private bool isPlayingEffect = false;
    private void Start()
    {
        TVScreen = transform.GetChild(0).gameObject;
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        if (isPlayingEffect) return;
        
        if(TVScreen.activeSelf == false) StartCoroutine(OnEffect());
        else if(TVScreen.activeSelf) StartCoroutine(OffEffect());
    }


    public override bool IsInteractionPossible()
    {
        return true;
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
