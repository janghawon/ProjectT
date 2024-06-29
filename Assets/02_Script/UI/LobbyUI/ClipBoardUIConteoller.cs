using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public class ClipBoardUIConteoller : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> _activeSetInfoEvent;
    private ClipBoardUI[] _clipBoardUIArr;

    private void Start()
    {
        _activeSetInfoEvent?.Invoke(false);
        _clipBoardUIArr = GetComponentsInChildren<ClipBoardUI>();

        foreach(var cb in _clipBoardUIArr)
        {
            cb.OnSelect += HandleClickEvent;
            cb.ExitLabel.OnClickEvent += AllGenerateTrm;
        }
    }

    private void AllGenerateTrm(UIObject obj)
    {
        foreach (var cb in _clipBoardUIArr)
        {
            cb.NormalPositioning();
        }
    }

    private void HandleClickEvent(Transform trm)
    {
        foreach (var cb in _clipBoardUIArr)
        {
            cb.CanInteractable = false;

            if(cb.transform != trm)
            {
                cb.ActiveFalse();
            }
        }

        _activeSetInfoEvent?.Invoke(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(trm.DOLocalMove(new Vector3(0, 3, -895), 0.3f));
        seq.Join(trm.DOLocalRotate(new Vector3(90, 0, -180), 0.3f));
        seq.Join(trm.DOScale(30, 0.3f));
    }
}
