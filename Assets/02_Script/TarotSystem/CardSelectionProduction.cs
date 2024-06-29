using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionProduction : MonoBehaviour
{
    public void DisappearCardProduction(TarotCard target, TarotCard[] tarotArr)
    {
        foreach(TarotCard card in tarotArr)
        {
            Transform trm = card.transform;
            card.SetLabelText(string.Empty, string.Empty);

            if(card == target)
            {
                SelectionProduction(trm);
                continue;
            }

            UnSelectionProduction(trm);
        }
    }

    private void SelectionProduction(Transform trm)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(trm.DOScale(1.3f, 0.5f).SetEase(Ease.OutBack));
        seq.Join(trm.DOLocalRotateQuaternion(Quaternion.identity, 0.2f));
        seq.AppendInterval(0.3f);
        seq.Append(trm.DOLocalMoveY(100, 0.3f));
        seq.Append(trm.DOLocalMoveY(-900, 0.6f).SetEase(Ease.InQuart));
    }

    private void UnSelectionProduction(Transform trm)
    {
        trm.DOLocalMoveY(-900, 0.5f).SetEase(Ease.OutBack);
    }
}
