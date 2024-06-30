using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTarotCardEffection : MonoBehaviour
{
    public Action OnTarotEffection { get; set; }

    public void ApplyTarotEffection(int tarotID)
    {
        TarotCardInfo info = TarotManager.Instance.GetTarotInfoByID(tarotID);
        TurnManager.Instance.OnGoldTimeStart += info.ApplyTarotEffect;
        TurnManager.Instance.OnGoldTimeStart += HandleStartDestinationUI;
    }

    private void HandleStartDestinationUI()
    {
        StartCoroutine(LookContentCo());
    }

    private IEnumerator LookContentCo()
    {
        InGameUIContent content = UIManager.Instance.GetSceneUIContent<InGameUIContent>();

        content.EnableContent(InGameType.destination);
        yield return new WaitForSeconds(3);
        content.EnableContent(InGameType.none);
    }
}
