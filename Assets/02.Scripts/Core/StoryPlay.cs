using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (Input.GetKey(KeyCode.Return))
            StartGameScene(null);
    }
#endif


    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        DOVirtual.DelayedCall(1, () => _videoPlayer.Play());
        _videoPlayer.loopPointReached += StartGameScene;
    }

    void StartGameScene(VideoPlayer vp)
    {
        SceneLoadWithFade.Instance.LoadScene("Map");
    }
}