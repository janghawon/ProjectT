using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Class: TarotCardInfo
* Author: 장하원
* Created: 2024년 6월 20일 목요일
* Description: 타로 카드 정보
*/

public abstract class TarotCardInfo : ScriptableObject
{
    public Sprite visual;
    public string cardName;
    [TextArea] public string info;
    public abstract void ApplyTarotEffect();
}
