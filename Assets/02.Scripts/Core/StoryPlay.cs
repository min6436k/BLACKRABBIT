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
        switch (SceneManager.GetActiveScene().name)
        {
            case "Story":
                SceneLoadWithFade.Instance.LoadScene("Map");
                break;
            case "Ending":
                Cursor.lockState = CursorLockMode.None;
                GameManager.Instance = null;
                SceneLoadWithFade.Instance.LoadScene("Title");
                break;
        }
    }
}