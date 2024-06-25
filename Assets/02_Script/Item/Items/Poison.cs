using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : ItemInstance
{
    protected override void UseItem()
    {

        PlayerDataManager.Instance.SetAlcohol(GamePlayManager.Instance.EnemyClientId, AlcoholState.NotSafe);

    }

}
