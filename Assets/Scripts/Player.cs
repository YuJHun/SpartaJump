using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//PlayerController라던지 앞으로 만들 기능들을 담을 예정
public class Player : MonoBehaviour
{
    [Header("플레이어 컨트롤러")]
    public PlayerController playerController;
    public PlayerCondition condition;

    public ItemData itemData; // 아이템 데이터
    public Action addItem; // 아이템 추가 액션

    public Transform dropItemPosition; // 아이템 드랍 위치

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        playerController =GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
