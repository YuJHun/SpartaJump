using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColor : MonoBehaviour
{
    public Color newColor = Color.red; // 바꾸고 싶은 색 지정 (인스펙터에서 변경 가능)
    // Start is called before the first frame update
    void Start()
    {
        // 현재 큐브의 Renderer에서 Material 가져와서 색 변경
        GetComponent<Renderer>().material.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
