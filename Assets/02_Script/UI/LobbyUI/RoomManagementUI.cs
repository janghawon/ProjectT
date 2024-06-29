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
    [Header("Labels")]
    [SerializeField] private LabelModule _createRoomButton;
    [SerializeField] private LabelModule _joinRoomButton;
    [SerializeField] private TextMeshProUGUI _joinCodeText;

    [Header("InputReaders")]
    [SerializeField] private TMP_InputField _roomNameInput;
    [SerializeField] private TMP_InputField _hostNameInput;
    [SerializeField] private TMP_InputField _joinCodeInput;
    [SerializeField] private TMP_InputField _guestNameInput;

    [Space(10)]

    // RoomName, HostName
    [SerializeField] private UnityEvent<string, string> _roomCreateEvent;
    // JoinCode, GuestName
    [SerializeField] private UnityEvent<string, string> _roomJoinEvent;

    private string _roomName;
    private string _hostName;
    private string _joinCode;
    private string _guestName;

    private void Awake()
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
        _roomNameInput.onSelect.AddListener((value) => HandleDestroyCaret(_roomNameInput.transform));

        _hostNameInput.onEndEdit.AddListener((value) => { _hostName = value; });
        _hostNameInput.onSelect.AddListener((v) => HandleDestroyCaret(_hostNameInput.transform));

        _joinCodeInput.onEndEdit.AddListener((value) => { _joinCode = value; });
        _joinCodeInput.onSelect.AddListener((v)=>  HandleDestroyCaret(_joinCodeInput.transform));

        _guestNameInput.onEndEdit.AddListener((varlue) => { _guestName = varlue; });
        _guestNameInput.onSelect.AddListener((v) => HandleDestroyCaret(_guestNameInput.transform));

        _joinCodeText.enabled = false;
        #endregion
    }

    private void HandleDestroyCaret(Transform trm)
    {
        GameObject calert = trm.Find("Text Area").Find("Caret").gameObject;

        if(calert != null)
        {
            Destroy(calert);
        }
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

    public void HandleSetJoinCode(string joinCode)
    {
        _joinCodeText.enabled = true;
        _joinCodeText.text = joinCode;
    }
}
