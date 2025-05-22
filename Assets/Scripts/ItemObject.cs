using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IInteractable // �������̽� ����
{
    public string GetInteractPrompt();// ��ȣ�ۿ� ������Ʈ�� ��ȯ�ϴ� �޼���
    public void OnInteract();// ��ȣ�ۿ� �� ȣ��Ǵ� �޼���
}


public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData itemData; // ������ ������
    public string GetInteractPrompt() // ��ȣ�ۿ� ������Ʈ ��ȯ
    {
        string str = $"{itemData.itemName}\n{itemData.itemDescription}";
        return str;
    }
    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = itemData;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject); // ������ ������Ʈ ����
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
