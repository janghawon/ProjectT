using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public class SurrenderSystem : SimpleButtonConnector
{
    [SerializeField] private UnityEvent _surrenderPanelActiveEvent;

    private void Start()
    {
        ConnectionBtn("SurrenderBtn");
    }

    protected override void BtnClickCallback(UIObject obj)
    {
        _surrenderPanelActiveEvent?.Invoke();
    }

    protected override void BtnDesecendCallback(UIObject obj)
    {
        obj.transform.DOKill();
        obj.transform.DOScale(1f, 0.1f);
    }

    protected override void BtnHoverCallback(UIObject obj)
    {
        obj.transform.DOKill();
        obj.transform.DOScale(1.1f, 0.1f);
    }
}
