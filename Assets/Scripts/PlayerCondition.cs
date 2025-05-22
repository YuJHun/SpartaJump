using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition hp { get { return uiCondition.hp; } }
    Condition mp { get { return uiCondition.mp; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp.SubValue(hp.passiveValue * Time.deltaTime);

        //Debug.Log("HP is "+ hp.curValue);
        if (hp.curValue == 0)
        {
            Die();
        }
    }
    void Die()
    {
        //Debug.Log("Die");
    }

    void heal(float amount)
    {
        hp.AddValue(amount);
        Debug.Log("Heal");
    }
}
