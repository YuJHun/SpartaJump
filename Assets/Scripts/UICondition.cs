using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition hp; // Condition 스크립트의 인스턴스
    public Condition mp; // Condition 스크립트의 인스턴스






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
