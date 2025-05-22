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

    ItemData selectedItem;
    int selectedItemIndex = 0;
    //private int curEquipIndex;

    private PlayerController controller;
    private PlayerCondition condition;

    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.playerController;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropItemPosition;

        controller.openInventory += Toggle; // 인벤토리 열기 액션 등록

        CharacterManager.Instance.Player.addItem += AddItem;  // 아이템 파밍 시
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
        //selectedItem = null;

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
        ItemData data = CharacterManager.Instance.Player.itemData;
        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }
        // 빈 슬롯 찾기
        ItemSlot emptySlot = GetEmptySlot();

        // 빈 슬롯이 있다면
        if (emptySlot != null)
        {
            emptySlot.itemData = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        // 빈 슬롯 마저 없을 때
        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }
    // 여러개 가질 수 있는 아이템의 정보 찾아서 return
    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemData == data && slots[i].quantity < data.maxStack)
            {
                return slots[i];
            }
        }
        return null;
    }
    // UI 정보 새로고침
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // 슬롯에 아이템 정보가 있다면
            if (slots[i].itemData != null)
            {
                slots[i].SetItem();
            }
            else
            {
                slots[i].ClearItem();
            }
        }
    }
    // 슬롯의 item 정보가 비어있는 정보 return
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemData == null)
            {
                return slots[i];
            }
        }
        return null;
    }
    // 아이템 버리기 (실제론 매개변수로 들어온 데이터에 해당하는 아이템 생성)
    public void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }
    public void SelectItem(int index)
    {
        if (slots[index].itemData == null) return;
        selectedItem = slots[index].itemData;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.itemName;
        selectedItemDescription.text = selectedItem.itemDescription;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for (int i =0; i< selectedItem.consumbales.Length;i++)
        {

            selectedItemStatName.text += selectedItem.consumbales[i].type.ToString()+"\n";
            selectedItemStatValue.text += selectedItem.consumbales[i].value.ToString() + "\n";

        }
        useButton.SetActive(selectedItem.itemType == itemType.Consumable);
        equipButton.SetActive(selectedItem.itemType == itemType.Equipable && !slots[index].equipped);
        unEquipButton.SetActive(selectedItem.itemType == itemType.Equipable&& slots[index].equipped);
        dropButton.SetActive(true);


    }
}

