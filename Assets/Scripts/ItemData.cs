using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum itemType // 아이템 종류를 정의하는 열거형
{
    Equipable,
    Consumable,
    Resourse,
    QuestItem
}

public enum ConsumableType // 소비 아이템 종류를 정의하는 열거형
{
    HealthPotion,
    ManaPotion,
    StaminaPotion,
    BuffPotion
}

[Serializable]
public class ItemDataConsumbale
{
    public ConsumableType type;
    public float value; // 소비 아이템의 효과를 나타내는 값
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
//ScriptableObject 클래스 위에 붙이면, Unity Project 창에서 우클릭 → Create 메뉴에 새 항목이 생겨요
public class ItemData : ScriptableObject
//ScriptableObject = Unity에서 데이터를 저장하고 재사용하기 위한 가볍고 효율적인 데이터 컨테이너 클래스
{
    [Header("Info")]
    public string itemName; // 아이템 이름
    public string itemDescription; // 아이템 설명
    public itemType itemType; // 아이템 종류
    public GameObject dropPrefab; // 아이템 드랍 프리팹

    [Header("Staking")]
    public bool canStack; // 스택 가능 여부
    public int maxStack; // 최대 스택 수

    [Header("Consumable")]
    public ItemDataConsumbale[] consumbales; // 소비 아이템의 효과를 나타내는 배열

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
