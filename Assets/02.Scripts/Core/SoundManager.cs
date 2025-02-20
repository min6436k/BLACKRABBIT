using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioClip[] audioClips;
    private Dictionary<string, AudioSource> _playingAudio = new Dictionary<string, AudioSource>();

    private void Start()
    {
        GameObject audioObj = new GameObject { name = "SoundManager" };
        audioObj.transform.parent = transform;
        foreach (AudioClip audioClip in audioClips)
        {
            AudioSource audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
            _playingAudio.Add(audioClip.name,audioSource);
        }
    }

    public void PlaySFX(string clipName, float volume = 1.0f, float pitch = 1.0f)
    {
        _playingAudio[clipName].pitch = pitch;
        _playingAudio[clipName].PlayOneShot(_playingAudio[clipName].clip, volume);
    }

}