using DG.Tweening;
using Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public class ActivationStore : ExtensionMono
{
    [SerializeField] private GameObject _volumeObj;
    [SerializeField] private UnityEvent _storeEnterEvent;
    [SerializeField] private UnityEvent _storeExitEvent;
    private CanvasGroup _storeChannel;

    private Action<UIObject> _onStoreActivation;

    private void Awake()
    {
        _storeChannel = GetComponent<CanvasGroup>();

        _storeChannel.alpha = 0;
        _storeChannel.interactable = false;
        _storeChannel.blocksRaycasts = false;

        _volumeObj.SetActive(false);

        _onStoreActivation += HandleEnterStore;
    }

    private void Start()
    {
        AddSetupCallback(ExitButtonSetUp);
    }

    public void ExitButtonSetUp()
    {
        ButtonModule exitBtn = FindUIObject<ButtonModule>(UIManager.Instance.GetUIKewordMask(UIKeyword.Button, UIKeyword.Panel, UIKeyword.Exit));
        exitBtn.OnClickEvent += HandleExitStore;
    }

    private void Update()
    {

        if (TurnManager.Instance == null) return;

        if (TurnManager.Instance.IsGoldTime) return;

        if(Input.GetKeyDown(KeyCode.Tab) && TurnManager.Instance.MyTurn)
        {
            _onStoreActivation?.Invoke(null);
        }
    }

    private void HandleEnterStore(UIObject obj)
    {
        _storeChannel.DOKill();

        _onStoreActivation -= HandleEnterStore;
        _onStoreActivation += HandleExitStore;

        _storeChannel.DOFade(1, 0.1f).OnComplete(() =>
        {
            _volumeObj.SetActive(true);
            _storeChannel.interactable = true;
            _storeChannel.blocksRaycasts = true;
        });

        _storeEnterEvent?.Invoke();
    }

    private void HandleExitStore(UIObject obj)
    {
        _storeChannel.DOKill();

        _onStoreActivation -= HandleExitStore;
        _onStoreActivation += HandleEnterStore;

        _storeChannel.DOFade(0, 0.1f).OnComplete(() =>
        {
            _volumeObj.SetActive(false);
            _storeChannel.interactable = false;
            _storeChannel.blocksRaycasts = false;
        });

        _storeExitEvent?.Invoke();
    }

    public void RegisterCallback(Action<UIObject> callback, UnityAction exitCallBack)
    {

        _onStoreActivation += callback;
        _storeExitEvent.AddListener(exitCallBack);

    }

}
