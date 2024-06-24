using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFunction;
using Extension;
using System;
using DG.Tweening;

public class MenuUISystem : ExtensionMono
{
    private ButtonObject _menuButton;
    private bool _onPressedMenuBtn;

    private void Start()
    {
        _menuButton = FindUIObject<ButtonObject>(UIManager.Instance.GetUIKewordMask(UIKeyword.Button, UIKeyword.Panel, UIKeyword.Setup));

        SetupMenuButton();
    }

    private void SetupMenuButton()
    {
        _menuButton.OnHoverEvent += HandleHoverMenuBtn;
        _menuButton.OnDesecendEvent += HandleDesecendMenuBtn;
        _menuButton.OnClickEvent += HandleClickMenuBtn;
    }

    private void HandleClickMenuBtn()
    {
        _onPressedMenuBtn = !_onPressedMenuBtn;

        if(_onPressedMenuBtn)
        {
            _menuButton.OnHoverEvent -= HandleHoverMenuBtn;
            _menuButton.OnDesecendEvent -= HandleDesecendMenuBtn;
        }
        else
        {
            _menuButton.OnHoverEvent += HandleHoverMenuBtn;
            _menuButton.OnDesecendEvent += HandleDesecendMenuBtn;
        }
    }

    private void HandleHoverMenuBtn()
    {
        _menuButton.transform.DOKill();

        _menuButton.transform.DOScale(1.1f, 0.2f);
        _menuButton.transform.DOLocalRotate(new Vector3(0, 0, 135), 0.3f).SetEase(Ease.OutQuart);
    }

    private void HandleDesecendMenuBtn()
    {
        _menuButton.transform.DOKill();

        _menuButton.transform.DOScale(1f, 0.2f);
        _menuButton.transform.DOLocalRotate(new Vector3(0, 0, 45), 0.3f).SetEase(Ease.OutQuart);
    }
}
