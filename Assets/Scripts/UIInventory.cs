using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Progress;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public GameObject inventoryWindow;

    [Header("Selected Item")]           // 선택한 슬롯의 아이템 정보 표시 위한 UI
    public Transform slotPanel;
    public Transform dropPosition;      // item 버릴 때 필요한 위치

    private ItemSlot selectedItem;
    private int selectedItemIndex;

    [Header("아이템 정보")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    [Header("버튼")]
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;


    private int curEquipIndex;

    private PlayerController controller;
    private PlayerCondition condition;
    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.playerController;
        condition = CharacterManager.Instance.Player.condition;

        controller.openInventory += Toggle; // 인벤토리 열기 액션 등록

        // Inventory UI 초기화 로직들
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].uiInventory = this;
            //slots[i].Clear();
        }
        ClearSelectedItemWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 선택한 아이템 표시할 정보창 Clear 함수
    void ClearSelectedItemWindow()
    {
        selectedItem = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }
    // Inventory 창 Open/Close 시 호출
    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    void AddItem()
    {
        ItemData data =CharacterManager.Instance.Player.itemData;
    }

}
