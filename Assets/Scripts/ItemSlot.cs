using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour
{
    public ItemData itemData;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quatityText;
    private Outline outline;


    public UIInventory uiInventory;

    public int index;
    public bool equipped;
    public int quantity;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem()
    {

        icon.gameObject.SetActive(true);
        icon.sprite = itemData.itemIcon;
        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void ClearItem()
    {
        itemData = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }
    public void OnClickButton()
    {
            uiInventory.SelectItem(index); 
    }
}
