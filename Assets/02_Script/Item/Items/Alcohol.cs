using System.Runtime.CompilerServices;
using UnityEngine;

public class Alcohol : ItemInstance
{
    protected override void UseItem()
    {

        var state = PlayerDataManager.Instance.Data.state;

        if (state == AlcoholState.Safe)
        {

            FeedbackManager.Instance.PlayFeedback("Yammy");

        }
        else
        {

            FeedbackManager.Instance.PlayFeedback("Aya");

        }

        PlayerDataManager.Instance.AddHealth(-(int)state);

        PlayerDataManager.Instance.SetAlcohol(RandomAlcohol());
        TurnManager.Instance.SkipTurn();

    }

    protected override void UseLinkItem()
    {



    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AlcoholState RandomAlcohol()
    {

        return (AlcoholState)Random.Range(0, 2);

    }

}
