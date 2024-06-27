using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFunction;
using Extension;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class MenuUISystem : ExtensionMono
{
    private Tween _visualMaskingTween;
    [SerializeField] Image _visualMask;
    private bool _onPressedMenuBtn;

    private void Start()
    {
        ButtonModule menuButton = FindUIObject<ButtonModule>(UIManager.Instance.GetUIKewordMask(UIKeyword.Button, UIKeyword.Panel, UIKeyword.Setup));

        menuButton.OnHoverEvent += HandleHoverMenuBtn;
        menuButton.OnDesecendEvent += HandleDesecendMenuBtn;
        menuButton.OnClickEvent += HandleClickMenuBtn;

        menuButton.OnClickEvent += HandleVisualMaskActive;
    }

    private void HandleVisualMaskActive(UIObject obj)
    {
        _visualMaskingTween?.Kill();

        _visualMaskingTween =
        DOTween.To(() => _visualMask.fillAmount, x => _visualMask.fillAmount = x, 1, 0.3f);

        obj.OnClickEvent -= HandleVisualMaskActive;
        obj.OnClickEvent += HandleVisualMaskHide;
    }

    private void HandleVisualMaskHide(UIObject obj)
    {
        _visualMaskingTween?.Kill();

        _visualMaskingTween =
        DOTween.To(() => _visualMask.fillAmount, x => _visualMask.fillAmount = x, 0, 0.3f);

        obj.OnClickEvent += HandleVisualMaskActive;
        obj.OnClickEvent -= HandleVisualMaskHide;
    }

    private void HandleClickMenuBtn(UIObject obj)
    {
        _onPressedMenuBtn = !_onPressedMenuBtn;

        if(_onPressedMenuBtn)
        {
            obj.OnHoverEvent -= HandleHoverMenuBtn;
            obj.OnDesecendEvent -= HandleDesecendMenuBtn;
        }
        else
        {
            obj.OnHoverEvent += HandleHoverMenuBtn;
            obj.OnDesecendEvent += HandleDesecendMenuBtn;
        }
    }
    private void HandleHoverMenuBtn(UIObject obj)
    {
        obj.transform.DOKill();

        obj.transform.DOScale(1.1f, 0.2f);
        obj.transform.DOLocalRotate(new Vector3(0, 0, 135), 0.3f).SetEase(Ease.OutQuart);
    }
    private void HandleDesecendMenuBtn(UIObject obj)
    {
        obj.transform.DOKill();

        obj.transform.DOScale(1f, 0.2f);
        obj.transform.DOLocalRotate(new Vector3(0, 0, 45), 0.3f).SetEase(Ease.OutQuart);
    }
}
