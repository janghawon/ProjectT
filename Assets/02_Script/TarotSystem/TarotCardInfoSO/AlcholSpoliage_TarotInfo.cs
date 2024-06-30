using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TarotCardInfo/AlcholSpoliage")]
public class AlcholSpoliage_TarotInfo : TarotCardInfo
{
    public override void ApplyTarotEffect()
    {
        TurnManager.Instance.OnTurnChanged += HandleRandomChangeAlcholState;
    }

    public void HandleRandomChangeAlcholState(ulong oldID, ulong newID)
    {
        AlcoholState state = (AlcoholState)Random.Range(0, 2);
        PlayerDataManager.Instance.SetAlcohol(newID, state);
    }
}
