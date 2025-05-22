using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;//몇 초마다 한 번만 Ray를 쏠지 정하는 값
    private float lastCheckTime;//마지막으로 Ray를 쏜 시간
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("curInteractable: " + curInteractable);
        Debug.Log("promptText: " + promptText);
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            //ScreenPointToRay = 화면 중심에서 카메라 방향으로 Ray를 쏘는 함수
            //maxCheckDistance = Ray가 얼마나 멀리까지 닿을 수 있나를 설정하는 값
            //collider = Ray가 맞은 *오브젝트의 Collider(충돌체)*를 뜻해요
            //curInteractGameObject = 지금 Ray에 맞은 오브젝트를 기억해두는 변수
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                //string hitLayerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
                //Debug.Log("맞은 오브젝트 레이어: " + hitLayerName);
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        if (promptText == null)
        {
            Debug.LogError("promptText가 연결되지 않았습니다!");
            return;
        }

        if (curInteractable == null)
        {
            Debug.LogError("curInteractable이 null입니다!");
            return;
        }
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        Debug.Log("E키 눌림"); // 눌리는지 확인
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}