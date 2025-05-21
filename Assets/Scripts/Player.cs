using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PlayerController����� ������ ���� ��ɵ��� ���� ����
public class Player : MonoBehaviour
{
    [Header("�÷��̾� ��Ʈ�ѷ�")]
    public PlayerControler playerControler;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        playerControler =GetComponent<PlayerControler>();
    }
}
