using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antidote : ItemInstance
{
    protected override void UseItem()
    {

        PlayerDataManager.Instance.SetAlcohol(AlcoholState.Safe);
        AnimationManager.Instance.PlayInputAnimation(transform, () => Destroy(gameObject), true);

    }

    protected override void UseLinkItem()
    {
        AnimationManager.Instance.PlayTargetInputAnimation(transform, () => Destroy(gameObject), true);
    }
}
