using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController playerController;
    public Camera mainCamera;
    public CameraManager cameraManager;
    public EventManager eventManager;
    public GameFlags gameFlags;
    public UINavigation uINavigation => GetComponent<UINavigation>();
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
    }
}
