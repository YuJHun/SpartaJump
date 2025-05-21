using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [Header("������")]
    public float moveSpeed = 5f;
    private Vector2 nowMovementInput;//inputAction���� �޾ƿ� ��
    public float jumpPower = 5f;//���� ��
    public LayerMask groundLayerMask;//���̾� ����ũ


    [Header("����")]
    public Transform cameraContainer;//ī�޶��� �θ� ������Ʈ
    public float minXLook;//x�� ȸ������ �ּڰ�
    public float maxXLook;//x�� ȸ������ �ִ�
    private float camCurXRot =0f;//"ī�޶��� ���� X�� ȸ����"�� �����صδ� �뵵
    public float lookSensitivity;//���콺 ����
    private Vector2 mouseDelta;//���콺�� �������� �����ϴ� �뵵


    private Rigidbody _rigidbody;
    private void FixedUpdate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //�̷� ������ ��� Rigidbody�� ��� �� ���,  GetComponent<Rigidbody>() �� �Ź� �θ��� ����� ũ�� ��ȿ�����̾�.
        //���� Awake()���� �� ���� ã��, ������ �����صδ� ����!
    }
    private void LateUpdate()
    {
        CameraLook();//ī�޶� ȸ�� �Լ� ȣ��
    }




    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Ŀ�� �Ⱥ��̰Ե�
    }

    // Update is called once per frame
    void Update()
    {
        Move();//������ �Լ� ȣ��
    }

    public void OnMove(InputAction.CallbackContext �������)
    {
        if (�������.phase == InputActionPhase.Performed)//phase �б���
        {
            nowMovementInput = �������.ReadValue<Vector2>();//���� �о�´�

        }
        else if (�������.phase == InputActionPhase.Canceled)
        {
            nowMovementInput = Vector2.zero;//���� �ʱ�ȭ�Ѵ�
        }
    }
    void Move()
    {
        Vector3 dir = (transform.forward * nowMovementInput.y)      //�����ΰ��� �ڷΰ��� W S
                         + (transform.right * nowMovementInput.x);  //�¿�� ���� A D
        dir *= moveSpeed* Time.deltaTime;                          //�̵��ӵ�
        dir.y = _rigidbody.velocity.y;                              //y���� Rigidbody�� y�� �ӵ��� �����´�
        _rigidbody.velocity = dir;//�̵��ӵ��� Rigidbody�� �־��ش�
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);//x�� ȸ������ �����Ѵ�
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);//ī�޶��� ȸ������ �����Ѵ�

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);//�÷��̾��� ȸ������ �����Ѵ�
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

