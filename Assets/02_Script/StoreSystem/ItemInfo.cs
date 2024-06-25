using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Class: ItemInfo
* Author: ���Ͽ�
* Created: 2024�� 6�� 21�� �ݿ���
* Description: ������ ���� 
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
