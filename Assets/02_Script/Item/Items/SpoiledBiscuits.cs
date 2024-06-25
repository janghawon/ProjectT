using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoiledBiscuits : ItemInstance
{
    protected override void UseItem()
    {

        if(Random.value > 0.5f)
        {

            PlayerDataManager.Instance.AddHealth(1);

        }

    }

}
