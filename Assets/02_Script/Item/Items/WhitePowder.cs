using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePowder : ItemInstance
{
    protected override void UseItem()
    {

        var state = PlayerDataManager.Instance[GamePlayManager.Instance.EnemyClientId].state;

        if(state != AlcoholState.Safe)
        {

            PlayerDataManager.Instance.SetAlcohol(GamePlayManager.Instance.EnemyClientId, state);

        }

    }

}
