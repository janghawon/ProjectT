using DG.Tweening;
using Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public class ClipBoardUI : ExtensionMono
{
    [SerializeField] private UnityEvent _hoverEvent;
    [SerializeField] private UnityEvent _descendEvent;
    [SerializeField] private UnityEvent _clickEvent;

    public bool CanInteractable { get; set; } = true;
    public Action<Transform> OnSelectAction { get; set; }

    private void OnMouseEnter()
    {
        if(!CanInteractable) return;

        transform.DOKill();
        transform.DOScale(26f, 0.2f);

        _hoverEvent?.Invoke();
    }

    private void OnMouseExit()
    {
        if (!CanInteractable) return;

        transform.DOKill();
        transform.DOScale(25f, 0.2f);

        _descendEvent?.Invoke();
    }

    private void OnMouseDown()
    {
        OnSelectAction?.Invoke(transform);
        _clickEvent?.Invoke();
    }

    public void ActiveFalse()
    {
        transform.DOLocalMoveY(-37, 0.1f).SetEase(Ease.OutBack);
    }
}
