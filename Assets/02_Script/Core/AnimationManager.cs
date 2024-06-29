using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoSingleton<AnimationManager>
{

    [SerializeField] private Transform _inputTrm, _enemyInputTrm;
    [SerializeField] private Transform _eatPos;

    /// <summary>
    /// 술에 뭐 넣는 애니메이션
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
    /// 먹는 애니메이션
    /// </summary>
    public void PlayEatAnimation(Transform obj, Action endCallback)
    {

        Sequence seq = DOTween.Sequence();

        seq.Append(obj.DOMove(_eatPos.position, 0.3f));
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => endCallback?.Invoke());

    }

}
