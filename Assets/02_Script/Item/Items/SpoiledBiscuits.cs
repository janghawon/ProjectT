using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoiledBiscuits : ItemInstance
{
    protected override void UseItem()
    {

        AnimationManager.Instance.PlayEatAnimation(transform, () =>
        {

            if (Random.value > 0.5f)
            {

                FeedbackManager.Instance.PlayFeedback("Yammy");
                PlayerDataManager.Instance.AddHealth(1);

            }
            else
            {


                FeedbackManager.Instance.PlayFeedback("Aya");

            }

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
