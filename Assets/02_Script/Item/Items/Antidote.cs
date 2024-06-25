using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antidote : ItemInstance
{
    protected override void UseItem()
    {

        PlayerDataManager.Instance.SetAlcohol(AlcoholState.Safe);

    }

}
