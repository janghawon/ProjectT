using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVProduction : PoolableMono
{
    [SerializeField] private Image _circleLight;
    [SerializeField] private Image _horizontalLight;

    private Sequence _productionSeq;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            PlayPowerOnProduction(Color.white, 1);
        }
    }

    public void PlayPowerOnProduction(Color lightColor, float alpha = 0)
    {
        _productionSeq?.Kill();
        ResetProduction(lightColor, alpha);

        _productionSeq = DOTween.Sequence();

        _productionSeq.Append(_circleLight.DOFade(alpha, 0.15f));
        _productionSeq.Append(_horizontalLight.DOFade(alpha, 0.15f).SetEase(Ease.InExpo));
        _productionSeq.Join(_horizontalLight.transform.DOScaleX(970, 0.2f).SetEase(Ease.InExpo));
        _productionSeq.Append(_horizontalLight.transform.DOScaleY(550, 0.2f).SetEase(Ease.InExpo));
        _productionSeq.Append(_circleLight.DOFade(0, 0.2f));
        _productionSeq.Join(_horizontalLight.DOFade(0, 0.2f));
    }

    public void PlayPowerOffProduction(Color lightColor, float alpha = 1)
    {
        _productionSeq?.Kill();
        ResetProduction(lightColor, alpha);

        _horizontalLight.transform.localScale = new Vector2(970, 550);

        _productionSeq = DOTween.Sequence();

        _productionSeq.Append(_horizontalLight.transform.DOScaleY(2, 0.2f).SetEase(Ease.InExpo));
        _productionSeq.Append(_horizontalLight.transform.DOScaleX(2, 0.2f).SetEase(Ease.InExpo));
        _productionSeq.Append(_circleLight.DOFade(0, 0.1f));
        _productionSeq.Join(_horizontalLight.DOFade(0, 0.1f));
    }

    private void ResetProduction(Color lightColor, float alpha)
    {
        lightColor.a = alpha;

        _circleLight.color = lightColor;
        _horizontalLight.color = lightColor;
    }

    public override void Init()
    {
        ResetProduction(Color.white, 0);
    }
}
