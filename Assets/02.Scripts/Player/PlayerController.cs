using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public enum PlayerState
{
    Idle,
    Interact,
    JumpScare,
    Option
}
    

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; set; } = 2;
    public PlayerState CurrentState { get; set; } = PlayerState.Idle;

    [SerializeField] private float _acceleration = 4;

    //GetAxis 입력 중간 저장
    private float _inputX, _inputY;
    
    //인풋을 실사용하는 변수들
    //카메라에서 움직임 상태를 참조하기 위해 프로퍼티 사용
    private Vector3 _moveInput;
    private Vector3 _moveDir;
    private float _dirSpeed;
    public CharacterController Controller { get; private set; }
    public PlayableAsset[] jumpScares;
    
    private PlayableDirector _playableDirector;
    
    
    void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(CurrentState == PlayerState.Idle)
            Move();
    }

    void Move()
    {
        if (Input.GetKey(Setting.CurrentKeyValues[EKeyInputs.Up]))
            _inputY = Mathf.MoveTowards(_inputY, 1f, Time.deltaTime * _acceleration);
        else if (Input.GetKey(Setting.CurrentKeyValues[EKeyInputs.Down]))
            _inputY = Mathf.MoveTowards(_inputY, -1f, Time.deltaTime * _acceleration);
        else 
            _inputY = Mathf.MoveTowards(_inputY, 0f, Time.deltaTime * _acceleration);
        
        if (Input.GetKey(Setting.CurrentKeyValues[EKeyInputs.Left]))
            _inputX = Mathf.MoveTowards(_inputX, -1f, Time.deltaTime * _acceleration);
        else if (Input.GetKey(Setting.CurrentKeyValues[EKeyInputs.Right]))
            _inputX = Mathf.MoveTowards(_inputX, 1f, Time.deltaTime * _acceleration);
        else
            _inputX = Mathf.MoveTowards(_inputX, 0f, Time.deltaTime * _acceleration);
        
        // (_inputX, _inputY) = (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        //인풋을 벡터로 저장
        _moveInput = (transform.forward*_inputY + transform.right*_inputX);
        
        //normalize하여 이동 방향만 추출
        _moveDir = _moveInput.normalized;
        
        //magnitude 사용하여 대각선 이동 루트2 속도 제한
        _dirSpeed = Mathf.Min(_moveInput.magnitude, 1.0f) * MoveSpeed*Time.deltaTime;

        //두 값을 합쳐 자연스러운 이동속도 구현
        Controller.Move(_moveDir * _dirSpeed);
    }

    public void JumpScareTimeLine(int n)
    {
        CurrentState = PlayerState.JumpScare;
        _playableDirector.playableAsset = jumpScares[n];
        _playableDirector.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndingTrigger"))
        {
            CurrentState = PlayerState.Interact;
            GameManager.Instance.cameraManager.EndingLight();
            DOVirtual.DelayedCall(4, ()=>SceneLoadWithFade.Instance.LoadScene("Ending"));
        }
    }
}
