﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("움직임")]
    public float moveSpeed = 5f;
    private Vector2 nowMovementInput;//inputAction에서 받아온 값
    public float jumpPower = 5f;//점프 힘
    public LayerMask groundLayerMask;//레이어 마스크


    [Header("보다")]
    public Transform cameraContainer;//카메라의 부모 오브젝트
    public float minXLook;//x의 회전범위 최솟값
    public float maxXLook;//x의 회전범위 최댓값
    private float camCurXRot = 0f;//"카메라의 현재 X축 회전값"을 저장해두는 용도
    public float lookSensitivity;//마우스 감도
    private Vector2 mouseDelta;//마우스의 움직임을 저장하는 용도
    public bool canLook = true;//카메라를 회전할 수 있는지 여부를 저장하는 용도

    public Action openInventory;//인벤토리 열기 액션
    private Rigidbody _rigidbody;





    private void FixedUpdate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //이런 식으로 계속 Rigidbody를 써야 할 경우,  GetComponent<Rigidbody>() 를 매번 부르면 비용이 크고 비효율적이야.
        //래서 Awake()에서 한 번만 찾고, 변수에 저장해두는 거지!
    }
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();//카메라 회전 함수 호출
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //커서 안보이게됨
        StartCoroutine(DelayedLog());
    }

    // Update is called once per frame
    void Update()
    {
        Move();//움직임 함수 호출
    }
    private IEnumerator DelayedLog()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // 2초마다 실행
            Debug.Log("현재 스피드: " + moveSpeed);
        }

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)//phase 분기점
        {
            nowMovementInput = context.ReadValue<Vector2>();//값을 읽어온다

        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            nowMovementInput = Vector2.zero;//값을 초기화한다
        }
    }
    void Move()
    {
        Vector3 dir = (transform.forward * nowMovementInput.y)      //앞으로가고 뒤로가고 W S
                         + (transform.right * nowMovementInput.x);  //좌우로 가고 A D
        dir *= moveSpeed * Time.deltaTime;                          //이동속도
        dir.y = _rigidbody.velocity.y;                              //y축은 Rigidbody의 y축 속도를 가져온다
        _rigidbody.velocity = dir;//이동속도를 Rigidbody에 넣어준다
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);//x축 회전값을 제한한다
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);//카메라의 회전값을 조정한다

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);//플레이어의 회전값을 조정한다
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        //Debug.Log(IsGrounded());
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
                {
                new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
                new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
                new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
                new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
                };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.6f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            openInventory?.Invoke();
            ToggleCursor();
        }
    }
    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
    public void BoostSpeed(float amount, float duration)
    {
        StartCoroutine(SpeedBoostRoutine(amount, duration));
    }
    private IEnumerator SpeedBoostRoutine(float amount, float duration)
    {
        moveSpeed *= amount;
        Debug.Log("스피드 증가!");


        //yield return new WaitForSeconds(duration);// (duration)초 대기
        float timeLeft = duration;

        while (timeLeft > 0)
        {
            Debug.Log("남은 시간: " + Mathf.CeilToInt(timeLeft) + "초");
            yield return new WaitForSeconds(1f);// (duration)초 대기
            timeLeft -= 1f;
        }


        moveSpeed /= amount;
        Debug.Log("스피드 원래대로 복구됨!");
    }
    
}

