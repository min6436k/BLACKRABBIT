using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class StoryPlay : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartGameScene(null);
    }
#endif


    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        _videoPlayer.loopPointReached += StartGameScene;
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
        DOVirtual.DelayedCall(1, () => _videoPlayer.Play());
    }

    void StartGameScene(VideoPlayer vp)
    {
        if (SceneManager.GetActiveScene().name == "Story")
            SceneLoadWithFade.Instance.LoadScene("Map");
    }
}