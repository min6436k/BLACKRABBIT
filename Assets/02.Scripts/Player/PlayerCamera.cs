using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //마우스 감도
    public Vector2 mouseSensitivity;

    private PlayerController _playerController;
    private Transform _cameraFollowTR;
    private Animator _cameraAnim;

    private Vector2 _mouseInput;

    //SmoothDamp 보간을 위한 변수들
    [SerializeField] private float smoothTime = 0.1f;
    private Vector2 _currentVelocity;
    private Vector2 _currentRot;

    void Start()
    {
        //마우스 중앙 잠금
        Cursor.lockState = CursorLockMode.Locked;
        _playerController = GetComponent<PlayerController>();

        //태그or인덱스로 변경 예정
        _cameraFollowTR = transform.Find("CameraFollow");
        _cameraAnim = _cameraFollowTR.GetComponent<Animator>();
    }

    void Update()
    {
        if (_playerController.CurrentState == PlayerState.Idle)
            Rotate();
    }

    void FixedUpdate()
    {
        HeadBobing();
    }

    private void Rotate()
    {
        //Input받기
        _mouseInput.x += Input.GetAxis("Mouse X") * mouseSensitivity.x;
        _mouseInput.y -= Input.GetAxis("Mouse Y") * mouseSensitivity.y;
        _mouseInput.y = Mathf.Clamp(_mouseInput.y, -90f, 90f);

        //SmoothDamp 적용
        _currentRot.x = Mathf.SmoothDamp(_currentRot.x, _mouseInput.x, ref _currentVelocity.x, smoothTime);
        _currentRot.y = Mathf.SmoothDamp(_currentRot.y, _mouseInput.y, ref _currentVelocity.y, smoothTime);

        //상하는 카메라만, 좌우는 캐릭터가 회전
        transform.rotation = Quaternion.Euler(0, _currentRot.x, 0);
        _cameraFollowTR.localRotation = Quaternion.Euler(_currentRot.y, 0, 0f);
    }

    private void HeadBobing()
    {
        //플레이어가 평소 상태이고 움직이고 있을 시
        bool anableMove = _playerController.CurrentState == PlayerState.Idle && _playerController.Controller.velocity.magnitude > 0.3f;
        _cameraAnim.SetBool("bHeadBobing",anableMove);
    }
}