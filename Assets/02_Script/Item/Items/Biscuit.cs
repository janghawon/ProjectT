using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biscuit : ItemInstance
{
    protected override void UseItem()
    {

        AnimationManager.Instance.PlayEatAnimation(transform, () =>
        {

            FeedbackManager.Instance.PlayFeedback("Yamy");
            PlayerDataManager.Instance.AddHealth(1);
            Destroy(gameObject);

        });

    }

    protected override void UseLinkItem()
    {

        AnimationManager.Instance.PlayTargetEatAnimation(transform, () =>
        {

            Destroy(gameObject);

        });


    }

}
