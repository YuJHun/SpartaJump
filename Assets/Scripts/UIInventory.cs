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

    [Header("Selected Item")]           // ������ ������ ������ ���� ǥ�� ���� UI
    public Transform slotPanel;
    public Transform dropPosition;      // item ���� �� �ʿ��� ��ġ




    [Header("������ ����")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    [Header("��ư")]
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

        controller.openInventory += Toggle; // �κ��丮 ���� �׼� ���

        CharacterManager.Instance.Player.addItem += AddItem;  // ������ �Ĺ� ��
        // Inventory UI �ʱ�ȭ ������
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
    // ������ ������ ǥ���� ����â Clear �Լ�
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
    // Inventory â Open/Close �� ȣ��
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
        // �� ���� ã��
        ItemSlot emptySlot = GetEmptySlot();

        // �� ������ �ִٸ�
        if (emptySlot != null)
        {
            emptySlot.itemData = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        // �� ���� ���� ���� ��
        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }
    // ������ ���� �� �ִ� �������� ���� ã�Ƽ� return
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
    // UI ���� ���ΰ�ħ
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // ���Կ� ������ ������ �ִٸ�
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
    // ������ item ������ ����ִ� ���� return
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
    // ������ ������ (������ �Ű������� ���� �����Ϳ� �ش��ϴ� ������ ����)
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

