using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option : UIView
{
    [SerializeField] AudioMixer audioMixer;
    
    [SerializeField] Slider sensitivitySlider;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    
    [SerializeField] TMP_Dropdown resolutionDropdown;
    
    [SerializeField] TMP_Text[] KeyTexts;
    private int _keyIndex = -1;
    
    private List<Resolution> resolutions = new List<Resolution>();
    private int _resolutionIndex = 3;
#if UNITY_EDITOR

    private bool _isFullScreen = false;
#else
    private bool _isFullScreen = Screen.fullScreen;
#endif

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
        resolutions.Add(new Resolution { width = 2560, height = 1440 });
        
        List<String> resolutionOptions = new List<string>();

        for (int i = 0; i < resolutions.Count; i++)
        {
            resolutionOptions.Add(resolutions[i].width + "x" + resolutions[i].height);
        }
        
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = _resolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
        Setting.CurrentKeyValues.Clear();
        
        Setting.CurrentKeyValues.Add(EKeyInputs.Up, KeyCode.W);
        Setting.CurrentKeyValues.Add(EKeyInputs.Left, KeyCode.A);
        Setting.CurrentKeyValues.Add(EKeyInputs.Down, KeyCode.S);
        Setting.CurrentKeyValues.Add(EKeyInputs.Right, KeyCode.D);
        Setting.CurrentKeyValues.Add(EKeyInputs.Interact, KeyCode.F);

        for (int i = 0; i < KeyTexts.Length; i++)
        {
            KeyTexts[i].text = Setting.CurrentKeyValues[(EKeyInputs)i].ToString();
        }
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
        _isFullScreen = isFullScreen;
        Screen.fullScreen = _isFullScreen;
    }

    public void SetKeyCode(int index)
    {
        _keyIndex = index;
    }

    void OnGUI()
    {
        Event e = Event.current;

        if (e.isKey && _keyIndex != -1)
        {
            if (e.keyCode != KeyCode.Escape)
            {
                Setting.CurrentKeyValues[(EKeyInputs)_keyIndex] = e.keyCode;
                KeyTexts[_keyIndex].text = Setting.CurrentKeyValues[(EKeyInputs)_keyIndex].ToString();
            }

            _keyIndex = -1;
        }
    }

    public override void Show()
    {
        base.Show();
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.playerController.CurrentState = PlayerState.Option;
    }
}
