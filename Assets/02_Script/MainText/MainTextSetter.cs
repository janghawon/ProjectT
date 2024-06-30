using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.UI;

public class MainTextSetter : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private LabelModule _mainLabel;
    [SerializeField] private LabelModule _subLabel;
    [SerializeField] private Image _line;
    [SerializeField] private Image _blackPanel;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Setting")]
    [SerializeField] private Color _lineColor;

    private void Start()
    {
        SetMainText("자유행동", "60초 동안 행동 할 수 있습니다.");
    }

    public void SetMainText(string mainText, string subText)
    {
        _blackPanel.color = new Color();
        _line.color = new Color();

        _mainLabel.SetText($"{{fade}}<dangle>{mainText}</>{{/}}");
        _subLabel.SetText($"{{fade}}<dangle>{subText}</>{{/}}");

        _line.DOColor(_lineColor, 0.2f);
        _blackPanel.DOColor(new Color(0, 0, 0, 0.8f), 0.2f);
    }

    public void FadeText(float value, float time)
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(value, time);
    }
}
