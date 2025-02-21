using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
