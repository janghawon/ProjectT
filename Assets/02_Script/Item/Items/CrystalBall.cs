using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBall : ItemInstance
{
    protected override void UseItem()
    {

        Debug.Log(PlayerDataManager.Instance.Data.state);

    }

}
