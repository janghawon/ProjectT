using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class HelpSystem : SimpleButtonConnector
{
    [SerializeField] private UnityEvent _helpPanelActiveEvent;

    private void Start()
    {
        ConnectionBtn("HelpBtn");
    }

    protected override void BtnClickCallback(UIObject obj)
    {
        _helpPanelActiveEvent?.Invoke();
    }

    protected override void BtnDesecendCallback(UIObject obj)
    {
        obj.transform.DOKill();
        obj.transform.DOScale(1.05f, 0.1f);
    }

    protected override void BtnHoverCallback(UIObject obj)
    {
        obj.transform.DOKill();
        obj.transform.DOScale(1.05f, 0.1f);
    }
}
