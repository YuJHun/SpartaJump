using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue;
    }

    void Update()
    {
        // fillAmount�� 0~1 ������ ������, ���� ä���� ������ ��Ÿ���ϴ�.
        uiBar.fillAmount = GePercentage();
    }
    float GePercentage()
    {
        // ���� ��(curValue)�� �ִ� ��(maxValue)���� �����ϴ� ������ ����ؼ� ��ȯ�մϴ�.
        // ��: curValue = 30, maxValue = 100 �̸�, 0.3�� ��ȯ�մϴ�.
        return curValue / maxValue;
    }
    public void AddValue(float value)
    {
        // ���� ���� value�� ���մϴ�.
        curValue += value;
        // ���� ���� ���� �ִ� ���� �ʰ��ϸ� �ִ� ������ �����մϴ�.
        if (curValue > maxValue)
        {
            curValue = maxValue;
        }
        //curValue = Mathf.Min(curValue+value, maxValue); ���� ���� ǥ��

    }
    public void SubValue(float value)
    {
        // ���� ������ value�� ���ϴ�.
        curValue -= value;
        // ���� ���� ���� 0���� ������ 0���� �����մϴ�.
        if (curValue < 0)
        {
            curValue = 0;
        }
    }
}
