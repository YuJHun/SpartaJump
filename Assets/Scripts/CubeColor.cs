using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColor : MonoBehaviour
{
    public Color newColor = Color.red; // �ٲٰ� ���� �� ���� (�ν����Ϳ��� ���� ����)
    // Start is called before the first frame update
    void Start()
    {
        // ���� ť���� Renderer���� Material �����ͼ� �� ����
        GetComponent<Renderer>().material.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
