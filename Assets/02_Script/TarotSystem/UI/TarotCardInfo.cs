using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Class: TarotCardInfo
* Author: ���Ͽ�
* Created: 2024�� 6�� 20�� �����
* Description: Ÿ�� ī�� ����
*/

public abstract class TarotCardInfo : ScriptableObject
{
    public int id;
    public Sprite visual;
    public string cardName;
    [TextArea] public string info;
    public abstract void ApplyTarotEffect();
}
