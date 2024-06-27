using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biscuit : ItemInstance
{
    protected override void UseItem()
    {

        PlayerDataManager.Instance.AddHealth(1);
        Destroy(gameObject);

    }

}
