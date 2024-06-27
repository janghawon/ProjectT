using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFunction;
using Extension;
using UnityEngine.Events;
using DG.Tweening;

public class SettingSystem : SimpleButtonConnector
{
    [SerializeField] private UnityEvent _settingPanelActiveEvent;

    private void Start()
    {
        ConnectionBtn("SettingBtn");
    }

    protected override void BtnClickCallback(UIObject obj)
    {
        _settingPanelActiveEvent?.Invoke();
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
