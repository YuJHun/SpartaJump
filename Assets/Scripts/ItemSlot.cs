using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour
{
    public ItemData itemData; // 슬롯에 들어있는 아이템 데이터

    public Button button; // 슬롯에 연결된 UI 버튼
    public Image icon; // 아이템 아이콘 이미지
    public TextMeshProUGUI quatityText; // 아이템 수량 표시용 텍스트
    private Outline outline; // 선택 시 외곽선 효과를 위한 컴포넌트

    public UIInventory uiInventory; // 상위 인벤토리 UI 참조

    public int index; // 이 슬롯이 인벤토리 리스트에서 몇 번째인지
    public bool equipped; // 장착 여부
    public int quantity; // 현재 이 아이템의 수량

    private void Awake()
    {
        outline = GetComponent<Outline>(); // 외곽선 컴포넌트 가져오기
    }

    private void OnEnable()
    {
        outline.enabled = equipped; // 슬롯이 활성화되면 장착 여부에 따라 외곽선 설정
    }

    void Start()
    {
        // 사용 안 함. 필요한 경우 초기화 코드 작성 가능
    }

    void Update()
    {
        // 사용 안 함. 실시간 변화 감지 필요 시 추가
    }

    /// <summary>
    /// 아이템 데이터를 바탕으로 UI 슬롯을 설정
    /// </summary>
    public void SetItem()
    {
        icon.gameObject.SetActive(true); // 아이콘 보이게 하기
        icon.sprite = itemData.itemIcon; // 아이콘 이미지 설정

        // 수량이 1보다 크면 숫자 표시, 아니면 빈 문자열
        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        // 외곽선은 equipped 값에 따라 표시
        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }

    /// <summary>
    /// 아이템 데이터를 제거하고 슬롯 UI 초기화
    /// </summary>
    public void ClearItem()
    {
        itemData = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }

    /// <summary>
    /// 이 슬롯의 버튼이 클릭되었을 때 호출되는 함수
    /// </summary>
    public void OnClickButton()
    {
        uiInventory.SelectItem(index); // 인벤토리에게 "이 슬롯이 선택됐음" 알림
    }
}
