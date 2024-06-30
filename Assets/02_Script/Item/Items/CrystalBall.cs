using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBall : ItemInstance
{
    protected override void UseItem()
    {

        Debug.Log(PlayerDataManager.Instance.Data.state);

        if(PlayerDataManager.Instance.Data.state == AlcoholState.Safe)
        {

            FeedbackManager.Instance.PlayFeedback("Yammy");

        }
        else
        {

            FeedbackManager.Instance.PlayFeedback("Aya");

        }

        Destroy(gameObject);

    }

    protected override void UseLinkItem()
    {

        Destroy(gameObject);

    }
}
