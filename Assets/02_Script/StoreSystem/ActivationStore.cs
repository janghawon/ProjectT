using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public class ActivationStore : MonoBehaviour
{
    [SerializeField] private GameObject _volumeObj;
    [SerializeField] private UnityEvent _storeEnterEvent;
    [SerializeField] private UnityEvent _storeExitEvent;
    private CanvasGroup _storeChannel;

    private Action _onStoreActivation;

    private void Awake()
    {
        _storeChannel = GetComponent<CanvasGroup>();

        _storeChannel.alpha = 0;
        _storeChannel.interactable = false;
        _storeChannel.blocksRaycasts = false;

        _volumeObj.SetActive(false);

        _onStoreActivation += HandleEnterStore;
    }

    public void ExitButtonSetUp(ButtonObject exitBtn)
    {
        exitBtn.OnClickEvent += HandleExitStore;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && TurnManager.Instance.MyTurn)
        {
            _onStoreActivation?.Invoke();
        }
    }

    private void HandleEnterStore()
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

    private void HandleExitStore()
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
}
