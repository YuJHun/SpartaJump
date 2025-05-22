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
        // fillAmount는 0~1 사이의 값으로, 바의 채워진 정도를 나타냅니다.
        uiBar.fillAmount = GePercentage();
    }
    float GePercentage()
    {
        // 현재 값(curValue)이 최대 값(maxValue)에서 차지하는 비율을 계산해서 반환합니다.
        // 예: curValue = 30, maxValue = 100 이면, 0.3을 반환합니다.
        return curValue / maxValue;
    }
    public void AddValue(float value)
    {
        // 현재 값에 value를 더합니다.
        curValue += value;
        // 만약 현재 값이 최대 값을 초과하면 최대 값으로 설정합니다.
        if (curValue > maxValue)
        {
            curValue = maxValue;
        }
        //curValue = Mathf.Min(curValue+value, maxValue); 위와 같은 표현

    }
    public void SubValue(float value)
    {
        // 현재 값에서 value를 뺍니다.
        curValue -= value;
        // 만약 현재 값이 0보다 작으면 0으로 설정합니다.
        if (curValue < 0)
        {
            curValue = 0;
        }
    }
}
