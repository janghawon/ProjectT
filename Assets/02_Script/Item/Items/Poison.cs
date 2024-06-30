using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : ItemInstance
{
    protected override void UseItem()
    {

        PlayerDataManager.Instance.SetAlcohol(GamePlayManager.Instance.EnemyClientId, AlcoholState.NotSafe);
        AnimationManager.Instance.PlayInputAnimation(transform, () => Destroy(gameObject), false);

    }

    protected override void UseLinkItem()
    {

        AnimationManager.Instance.PlayTargetInputAnimation(transform, () => Destroy(gameObject), false);

    }

}
