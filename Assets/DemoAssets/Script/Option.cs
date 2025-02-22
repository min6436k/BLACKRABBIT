using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    
    [SerializeField] Slider sensitivitySlider;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    
    [SerializeField] TMP_Dropdown resolutionDropdown;
    
    private List<Resolution> resolutions = new List<Resolution>();
    private int _resolutionIndex = 3;
    private bool _isFullScreen = true;

    private void Start()
    {
        audioMixer.GetFloat("Master", out float master);
        audioMixer.GetFloat("BGM", out float bgm);
        audioMixer.GetFloat("SFX", out float sfx);
        
        masterSlider.value = Mathf.Pow(10,master / 20);
        bgmSlider.value = Mathf.Pow(10 ,bgm / 20);
        sfxSlider.value = Mathf.Pow(10, sfx / 20);
        
        sensitivitySlider.value = Setting.MouseSensitivity;
        
        resolutionDropdown.ClearOptions();
        resolutions.Add(new Resolution { width = 960, height = 540 });
        resolutions.Add(new Resolution { width = 1280, height = 720 });
        resolutions.Add(new Resolution { width = 1600, height = 900 });
        resolutions.Add(new Resolution { width = 1920, height = 1080 });
        
        List<String> resolutionOptions = new List<string>();

        for (int i = 0; i < resolutions.Count; i++)
        {
            resolutionOptions.Add(resolutions[i].width + "x" + resolutions[i].height);
        }
        
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = _resolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            gameObject.SetActive(false);
    }
    
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        Setting.MouseSensitivity = sensitivity;
    }

    public void SetResolution(int resolutionIndex)
    {
        _resolutionIndex = resolutionIndex;
        Screen.SetResolution(resolutions[_resolutionIndex].width, resolutions[_resolutionIndex].height, _isFullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
