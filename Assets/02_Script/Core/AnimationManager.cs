using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoSingleton<AnimationManager>
{

    [SerializeField] private Transform _inputTrm, _enemyInputTrm;
    [SerializeField] private Transform _eatPos;
    [Header("_Enemy_")]
    [SerializeField] private Transform _targetInputTrm, _targetEnemyInputTrm;
    [SerializeField] private Transform _targetEatPos;

    /// <summary>
    /// ���� �� �ִ� �ִϸ��̼�
    /// </summary>
    public void PlayInputAnimation(Transform obj, Action endCallback, bool local)
    {

        Sequence seq = DOTween.Sequence();

        var pos = local ? _inputTrm : _enemyInputTrm;

        seq.Append(obj.DOMove(pos.position, 0.3f));
        seq.Append(obj.DORotate(new Vector3(0, 0, -110), 0.3f));
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => endCallback?.Invoke());

    }

    /// <summary>
    /// �Դ� �ִϸ��̼�
    /// </summary>
    public void PlayEatAnimation(Transform obj, Action endCallback)
    {

        Sequence seq = DOTween.Sequence();

        seq.Append(obj.DOMove(_eatPos.position, 0.3f));
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => endCallback?.Invoke());

    }

    /// <summary>
    /// ���� �� �ִ� �ִϸ��̼�
    /// </summary>
    public void PlayTargetInputAnimation(Transform obj, Action endCallback, bool local)
    {

        Sequence seq = DOTween.Sequence();

        var pos = local ? _targetInputTrm : _targetEnemyInputTrm;

        seq.Append(obj.DOMove(pos.position, 0.3f));
        seq.Append(obj.DORotate(new Vector3(0, 0, 110), 0.3f));
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => endCallback?.Invoke());

    }

    /// <summary>
    /// �Դ� �ִϸ��̼�
    /// </summary>
    public void PlayTargetEatAnimation(Transform obj, Action endCallback)
    {

        Sequence seq = DOTween.Sequence();

        seq.Append(obj.DOMove(_targetEatPos.position, 0.3f));
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => endCallback?.Invoke());

    }

}
