using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IInteractable // 인터페이스 정의
{
    public string GetInteractPrompt();// 상호작용 프롬프트를 반환하는 메서드
    public void OnInteract();// 상호작용 시 호출되는 메서드
}


public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData itemData; // 아이템 데이터
    public string GetInteractPrompt() // 상호작용 프롬프트 반환
    {
        string str = $"{itemData.itemName}\n{itemData.itemDescription}";
        return str;
    }
    public bool destroyOnInteract = true; // ✅ 에디터에서 설정 가능하게!
    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = itemData;
        CharacterManager.Instance.Player.addItem?.Invoke();
        if (destroyOnInteract)
        {
            Destroy(gameObject); // 아이템만 사라지게!
        }
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
