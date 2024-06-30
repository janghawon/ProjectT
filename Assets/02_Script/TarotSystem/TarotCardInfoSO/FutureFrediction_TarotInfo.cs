using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TarotCardInfo/FutureFrediction")]
public class FutureFrediction_TarotInfo : TarotCardInfo
{
    [Header("FutureFrediction")]
    [SerializeField] private ItemInfo _toGetitem;

    public override void ApplyTarotEffect()
    {
        GamePlayManager.Instance.AllClientGetItem(_toGetitem);
    }
}
