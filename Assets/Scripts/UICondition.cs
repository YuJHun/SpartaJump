using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition hp; // Condition ��ũ��Ʈ�� �ν��Ͻ�
    public Condition mp; // Condition ��ũ��Ʈ�� �ν��Ͻ�






    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
