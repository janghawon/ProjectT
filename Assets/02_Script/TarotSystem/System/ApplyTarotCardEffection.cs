using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTarotCardEffection : MonoBehaviour
{
    public Action OnTarotEffection { get; set; }
    public List<TarotCardInfo> _cards = new();

    public void ApplyTarotEffection(int tarotID)
    {
        GamePlayManager.Instance.AddTarot(tarotID);
    }

    public void ApplyTarotEffectLink(int tarotID)
    {

        TarotCardInfo info = TarotManager.Instance.GetTarotInfoByID(tarotID);
        TurnManager.Instance.OnGoldTimeStart += info.ApplyTarotEffect;
        TurnManager.Instance.OnGoldTimeStart += HandleStartDestinationUI;

        _cards.Add(info);

    }

    private void HandleStartDestinationUI()
    {
        StartCoroutine(LookContentCo());
    }

    private IEnumerator LookContentCo()
    {
        InGameUIContent content = UIManager.Instance.GetSceneUIContent<InGameUIContent>();

        content.EnableContent(InGameType.destination);
        FindObjectOfType<DestinationTarotSetter>().SetTarotInfo(_cards[0], _cards[1]);
        yield return new WaitForSeconds(5);
        content.EnableContent(InGameType.none);
    }
}
