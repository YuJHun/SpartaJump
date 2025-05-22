using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
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
    private float camCurXRot =0f;//"카메라의 현재 X축 회전값"을 저장해두는 용도
    public float lookSensitivity;//마우스 감도
    private Vector2 mouseDelta;//마우스의 움직임을 저장하는 용도


    private Rigidbody _rigidbody;
    private void FixedUpdate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //이런 식으로 계속 Rigidbody를 써야 할 경우,  GetComponent<Rigidbody>() 를 매번 부르면 비용이 크고 비효율적이야.
        //래서 Awake()에서 한 번만 찾고, 변수에 저장해두는 거지!
    }
    private void LateUpdate()
    {
        CameraLook();//카메라 회전 함수 호출
    }




    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //커서 안보이게됨
    }

    // Update is called once per frame
    void Update()
    {
        Move();//움직임 함수 호출
    }

    public void OnMove(InputAction.CallbackContext 현재상태)
    {
        if (현재상태.phase == InputActionPhase.Performed)//phase 분기점
        {
            nowMovementInput = 현재상태.ReadValue<Vector2>();//값을 읽어온다

        }
        else if (현재상태.phase == InputActionPhase.Canceled)
        {
            nowMovementInput = Vector2.zero;//값을 초기화한다
        }
    }
    void Move()
    {
        Vector3 dir = (transform.forward * nowMovementInput.y)      //앞으로가고 뒤로가고 W S
                         + (transform.right * nowMovementInput.x);  //좌우로 가고 A D
        dir *= moveSpeed* Time.deltaTime;                          //이동속도
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
}

