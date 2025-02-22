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

    private void Update()  
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadSceneAsync("Map");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += StartGameScene;
    }

    void StartGameScene(VideoPlayer vp)
    {
        SceneManager.LoadSceneAsync("Map");
    }
}