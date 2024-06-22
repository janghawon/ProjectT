using System;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;

public class ActivationStore : MonoBehaviour
{
    [SerializeField] private GameObject _volumeObj;
    private CanvasGroup _storeChannel;

    private void Awake()
    {
        _storeChannel = GetComponent<CanvasGroup>();

        _storeChannel.alpha = 0;
        _storeChannel.interactable = false;
        _storeChannel.blocksRaycasts = false;

        _volumeObj.SetActive(false);
    }
    public void ExitButtonSetUp(ButtonObject exitBtn)
    {
        exitBtn.OnClickEvent += HandleExitStore;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            HandleEnterStore();
        }
    }

    private void HandleEnterStore()
    {

    }

    private void HandleExitStore()
    {

    }
}
