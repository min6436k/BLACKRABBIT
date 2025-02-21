using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject Option;

    private void Start()
    {
        Option.SetActive(false);
    } 

    public void PlayButton()
    {
        SceneManager.LoadSceneAsync("Story");
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OptionButton()
    {
        Option.SetActive(true);
    }
}