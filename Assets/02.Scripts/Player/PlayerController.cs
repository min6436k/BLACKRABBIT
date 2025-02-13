using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    
    //GetAxis 입력 중간 저장
    private float _inputX, _inputY;
    
    //인풋을 실사용하는 변수들
    //카메라에서 움직임 상태를 참조하기 위해 프로퍼티 사용
    private Vector3 _moveInput;
    private Vector3 _moveDir;
    private float _dirSpeed;
    public CharacterController Controller { get; private set; }

    
    //후에 State등 이동 제한 구조(필요에 따라 hashset까지) 추가, PlayerCamera 애니메이션에 조건문 추가
    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        (_inputX, _inputY) = (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        //인풋을 벡터로 저장
        _moveInput = (transform.forward*_inputY + transform.right*_inputX);
        
        //normalize하여 이동 방향만 추출
        _moveDir = _moveInput.normalized;
        
        //magnitude 사용하여 대각선 이동 루트2 속도 제한
        _dirSpeed = Mathf.Min(_moveInput.magnitude, 1.0f) * moveSpeed*Time.deltaTime;

        //두 값을 합쳐 자연스러운 이동속도 구현
        Controller.Move(_moveDir * _dirSpeed);
    }
}
