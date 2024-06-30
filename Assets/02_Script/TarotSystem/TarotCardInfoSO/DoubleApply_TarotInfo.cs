using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TarotCardInfo/DoubleApplyInfo")]
public class DoubleApply_TarotInfo : TarotCardInfo
{
    public override void ApplyTarotEffect()
    {
        TurnManager.Instance.OnTurnChanged += HandleGetRandomItem;
    }

    private void HandleGetRandomItem(ulong oldId, ulong newId)
    {
        GamePlayManager.Instance.GetRandomItem();
    }
}
