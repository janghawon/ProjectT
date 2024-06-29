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
    [SerializeField] private LabelModule _exitLabel;
    [SerializeField] private UnityEvent _hoverEvent;
    [SerializeField] private UnityEvent _descendEvent;
    [SerializeField] private UnityEvent _clickEvent;
    [SerializeField] private UnityEvent _revertEvent;

    public bool CanInteractable { get; set; } = true;
    public Action<Transform> OnSelect { get; set; }
    public LabelModule ExitLabel => _exitLabel;

    private Vector3 _normalPos;
    private Quaternion _normalRot;
    private float _normalScale;

    private void Start()
    {
        _normalPos = transform.localPosition;
        _normalRot = transform.localRotation;
        _normalScale = transform.localScale.x;

        _exitLabel.gameObject.SetActive(false);
    }

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
        if (!CanInteractable) return;

        OnSelect?.Invoke(transform);
        _clickEvent?.Invoke();
        _exitLabel.gameObject?.SetActive(true);
    }

    public void ActiveFalse()
    {
        CanInteractable = false;
        transform.DOLocalMoveY(-37, 0.2f).SetEase(Ease.OutBack);
    }

    public void NormalPositioning()
    {
        transform.DOKill();

        transform.DOLocalMove(_normalPos, 0.2f).SetEase(Ease.OutBack);
        transform.DOLocalRotateQuaternion(_normalRot, 0.2f).SetEase(Ease.OutBack);
        transform.DOScale(_normalScale, 0.2f).OnComplete(() =>
        {
            CanInteractable = true;
        });

        _exitLabel.gameObject?.SetActive(false);
        _revertEvent?.Invoke();
    }
}
