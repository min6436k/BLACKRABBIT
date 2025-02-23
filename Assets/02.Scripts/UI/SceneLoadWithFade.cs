using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SceneLoadWithFade : MonoBehaviour
{
    public static SceneLoadWithFade Instance;
    private Tweener _fadeInTween;
    private Tweener _fadeOutTween;

    public float fadeTime = 1; 
    public Image _image;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _image = GetComponentInChildren<Image>();

        _fadeInTween = _image.DOFade(0, fadeTime).SetEase(Ease.InOutQuad).Pause().SetAutoKill(false);
        _fadeOutTween = _image.DOFade(1, fadeTime).SetEase(Ease.InOutQuad).Pause().SetAutoKill(false);

        SceneManager.sceneLoaded += sceneLoaded;
    }

    public void FadeOutIn()
    {
        _fadeOutTween.Rewind();
        _fadeOutTween.Play().OnComplete(()=>FadeIn());
    }

    public void FadeIn()
    {
        _image.color = Color.black;
        _fadeInTween.Rewind();
        _fadeInTween.Play();
    }
    
    public void FadeOut()
    {
        _fadeOutTween.Rewind();
        _fadeOutTween.Play();
    }

    private void sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        FadeIn();
    }

    public void LoadScene(string name)
    {
        _fadeOutTween.Rewind();
        _fadeOutTween.Play().OnComplete(() => SceneManager.LoadSceneAsync(name));
    }
}

