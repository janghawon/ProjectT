using DG.Tweening;
using Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public class RoomManagementUI : ExtensionMono
{
    [SerializeField] private LabelModule _createRoomButton;
    [SerializeField] private LabelModule _joinRoomButton;

    [SerializeField] private UnityEvent _roomCreateEvent;
    [SerializeField] private UnityEvent _roomJoinEvent;

    private void Start()
    {
        _createRoomButton.OnHoverEvent += HandleUpscaling;
        _joinRoomButton.OnHoverEvent += HandleUpscaling;

        _createRoomButton.OnDesecendEvent += HandleNormalScaling;
        _joinRoomButton.OnDesecendEvent += HandleNormalScaling;

        _createRoomButton.OnClickEvent += (obj) => _roomCreateEvent?.Invoke();
        _joinRoomButton.OnClickEvent += (obj) => _roomJoinEvent?.Invoke();
    }

    private void HandleUpscaling(UIObject obj)
    {
        obj.transform.DOKill();
        obj.transform.DOScale(1.1f, 0.2f);
    }

    private void HandleNormalScaling(UIObject obj)
    {
        obj.transform.DOKill();
        obj.transform.DOScale(1f, 0.2f);
    }
}
