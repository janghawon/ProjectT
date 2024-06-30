using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestinationTarotSetter : MonoBehaviour
{
    [SerializeField] private TarotCard _hostTarotCard;
    [SerializeField] private TarotCard _clientTarotCard;

    [SerializeField] private UnityEvent<TarotCard[]> _tarotProductionStartEvent;

    public void SetTarotInfo(TarotCardInfo hostInfo, TarotCardInfo clientInfo)
    {
        _hostTarotCard.SetLabelText(hostInfo.cardName, hostInfo.info);
        _clientTarotCard.SetLabelText(clientInfo.cardName, clientInfo.info);

        _tarotProductionStartEvent?.Invoke(new TarotCard[] { _hostTarotCard, _clientTarotCard });
    }
}
