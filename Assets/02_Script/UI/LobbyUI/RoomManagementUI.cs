using DG.Tweening;
using Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RoomManagementUI : ExtensionMono
{
    [SerializeField] private LabelModule _createRoomButton;
    [SerializeField] private LabelModule _joinRoomButton;

    [SerializeField] private TMP_InputField _roomNameInput;
    [SerializeField] private TMP_InputField _hostNameInput;
    [SerializeField] private TMP_InputField _joinCodeInput;
    [SerializeField] private TMP_InputField _guestNameInput;

    // RoomName, HostName
    [SerializeField] private UnityEvent<string, string> _roomCreateEvent;
    // JoinCode, GuestName
    [SerializeField] private UnityEvent<string, string> _roomJoinEvent;

    private string _roomName;
    private string _hostName;
    private string _joinCode;
    private string _guestName;

    private void Start()
    {
        #region BurttonSubcribe
        _createRoomButton.OnHoverEvent += HandleUpscaling;
        _joinRoomButton.OnHoverEvent += HandleUpscaling;

        _createRoomButton.OnDesecendEvent += HandleNormalScaling;
        _joinRoomButton.OnDesecendEvent += HandleNormalScaling;

        _createRoomButton.OnClickEvent += HandleCreateRoomEvent;
        _joinRoomButton.OnClickEvent += HandleJoinRoomEvent;
        #endregion
        #region Input Subcribe
        _roomNameInput.onEndEdit.AddListener((value) => { _roomName = value; });
        _hostNameInput.onEndEdit.AddListener((value) => { _hostName = value; });
        _joinCodeInput.onEndEdit.AddListener((value) => { _joinCode = value; });
        _guestNameInput.onEndEdit.AddListener((varlue) => { _guestName = varlue; });
        #endregion
    }


    private void HandleCreateRoomEvent(UIObject obj)
    {
        _roomCreateEvent?.Invoke(_roomName, _hostName);
    }

    private void HandleJoinRoomEvent(UIObject obj)
    {
        _roomJoinEvent?.Invoke(_joinCode, _guestName);
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
