using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TortureWall_TarotInfo : TarotCardInfo
{
    public override void ApplyTarotEffect()
    {
        TurnManager.Instance.OnTurnChanged += HandleDecountHealth;
    }

    private void HandleDecountHealth(ulong oldId, ulong newId)
    {
        int health = PlayerDataManager.Instance.GetHealth(newId) - 1;
        PlayerDataManager.Instance.SetHealth(newId, health);
    }
}
