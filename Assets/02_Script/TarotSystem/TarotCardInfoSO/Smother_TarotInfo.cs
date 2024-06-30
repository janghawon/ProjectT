using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TarotCardInfo/Smother")]
public class Smother_TarotInfo : TarotCardInfo
{
    public override void ApplyTarotEffect()
    {
        GamePlayManager.Instance.SetOpenItemServerRPC(false);
    }
}
