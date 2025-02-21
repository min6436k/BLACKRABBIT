using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController playerController;
    public CameraManager cameraManager;
    [HideInInspector] public UINavigation uINavigation;
    [HideInInspector] public SoundManager soundManager;
    public Canvas canvas;

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

        uINavigation = GetComponent<UINavigation>();
        soundManager = GetComponent<SoundManager>();
    }
}
