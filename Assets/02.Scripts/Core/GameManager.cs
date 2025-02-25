using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController playerController;
    public CameraManager cameraManager;
    public EventManager eventManager;
    public GameFlags gameFlags;
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
        
        // 키 초기값을 타이틀에서 하기에 맵 바로 실행하면 오류가 생겨 임시로 초기값 설정해줌
        if (Setting.CurrentKeyValues.Count < 5)
        {
            Setting.CurrentKeyValues.Add(EKeyInputs.Up, KeyCode.W);
            Setting.CurrentKeyValues.Add(EKeyInputs.Down, KeyCode.S);
            Setting.CurrentKeyValues.Add(EKeyInputs.Left, KeyCode.A);
            Setting.CurrentKeyValues.Add(EKeyInputs.Right, KeyCode.D);
            Setting.CurrentKeyValues.Add(EKeyInputs.Interact, KeyCode.F);
        }
    }
}
