using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum itemType // ������ ������ �����ϴ� ������
{
    Equipable,
    Consumable,
    Resourse,
    QuestItem
}

public enum ConsumableType // �Һ� ������ ������ �����ϴ� ������
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
    public float value; // �Һ� �������� ȿ���� ��Ÿ���� ��
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
//ScriptableObject Ŭ���� ���� ���̸�, Unity Project â���� ��Ŭ�� �� Create �޴��� �� �׸��� ���ܿ�
public class ItemData : ScriptableObject
//ScriptableObject = Unity���� �����͸� �����ϰ� �����ϱ� ���� ������ ȿ������ ������ �����̳� Ŭ����
{
    [Header("Info")]
    public string itemName; // ������ �̸�
    public string itemDescription; // ������ ����
    public itemType itemType; // ������ ����
    public GameObject dropPrefab; // ������ ��� ������

    [Header("Staking")]
    public bool canStack; // ���� ���� ����
    public int maxStack; // �ִ� ���� ��

    [Header("Consumable")]
    public ItemDataConsumbale[] consumbales; // �Һ� �������� ȿ���� ��Ÿ���� �迭

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
