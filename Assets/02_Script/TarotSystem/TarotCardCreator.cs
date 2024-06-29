using Extension;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
* Class: TarotCardCreator
* Author: 장하원
* Created: 2024년 6월 20일 목요일
* Description: 타로 카드 생성 UI
*/

public class TarotCardCreator : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Transform _backFacePrefab;
    [SerializeField] private TarotCard _tarotCardPrefab;
    [SerializeField] private TarotCardInfo[] _tarotCardInfoContainer;
    [SerializeField] private Transform[] _tarotCardTrmPosArr;

    private readonly int _canSelectTarotCardCount = 3;

    [Header("Events")]
    [SerializeField] private UnityEvent<Transform> _backFaceAppearEvent;
    [SerializeField] private UnityEvent<Transform> _backFaceDisAppearEvent;
    [SerializeField] private UnityEvent<Transform> _tarotCardAppearEvent;
    [SerializeField] private UnityEvent<TarotCard[]> _tarotCardProductionEvent;
    [SerializeField] private UnityEvent<TarotCard, TarotCard[]> _tarotCardSelectEvent;

    private void Start()
    {
        StartCoroutine(StartSelectingTarotCard());
    }

    private IEnumerator StartSelectingTarotCard()
    {
        Transform[] backFaceTrms = new Transform[_canSelectTarotCardCount];
        TarotCard[] tarotCards = new TarotCard[_canSelectTarotCardCount];

        _tarotCardInfoContainer.Shuffle();

        for(int i = 0; i < _canSelectTarotCardCount; i++)
        {
            Transform bf = Instantiate(_backFacePrefab, transform);
            bf.localPosition = _tarotCardTrmPosArr[i].localPosition;

            backFaceTrms[i] = bf;
            _backFaceAppearEvent?.Invoke(bf);
        }

        yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < _canSelectTarotCardCount; i++)
        {
            _backFaceAppearEvent?.Invoke(backFaceTrms[i]);
            yield return new WaitForSeconds(0.3f);
            _backFaceDisAppearEvent?.Invoke(backFaceTrms[i]);
            yield return new WaitForSeconds(0.15f);

            TarotCard tarot = Instantiate(_tarotCardPrefab, transform);
            tarot.transform.localPosition = _tarotCardTrmPosArr[i].localPosition;
            tarot.SetInfo(_tarotCardInfoContainer[i]);
            _tarotCardAppearEvent?.Invoke(tarot.VisualTrm);

            TarotCardInfo info = _tarotCardInfoContainer[i];
            tarot.SetLabelText(info.info, info.cardName);

            tarot.OnHoverEvent += tarot.EmphasizeLabelText;
            tarot.OnDesecendEvent += tarot.NormaingLabelText;

            tarot.OnClickEvent += (UIObject obj) =>
            PlayerDataManager.Instance.SetTarotInfo(tarot.Info.tarotID);

            tarot.OnClickEvent += (tarot) => 
            _tarotCardSelectEvent?.Invoke(tarot as TarotCard, tarotCards);

            tarotCards[i] = tarot;
        }

        yield return new WaitForSeconds(0.32f);
        _tarotCardProductionEvent?.Invoke(tarotCards);
    }
}
