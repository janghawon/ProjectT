using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Class: ItemInfo
* Author: 장하원
* Created: 2024년 6월 21일 금요일
* Description: 아이템 정보 
*/

[CreateAssetMenu(menuName = "SO/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    public string itemName;
    public Sprite visual;
    public int price;
    public GameObject prefab;
    [TextArea] public string info;
}
