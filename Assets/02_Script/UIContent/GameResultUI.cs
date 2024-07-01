using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFunction;
using UnityEngine.UI;
using System;
using Unity.Netcode;
using DG.Tweening;

public enum ResultType
{
    Win,
    Defeat,
    Draw
}

[Serializable]
public struct ResultInfoBlock
{
    public ResultType type;
    public string resultMainText;
    public string resultSubText;
    public Sprite icon;
}

public class GameResultUI : SceneUIContent
{
    [Header("Result Info")]
    [SerializeField] private ResultInfoBlock[] _resultArr;
    private Dictionary<ResultType, ResultInfoBlock> _resultDic = new();

    private Sequence _backLabelSeq;

    private void Awake()
    {
        foreach (ResultInfoBlock block in _resultArr)
        {
            _resultDic.Add(block.type, block);
        }
    }

    public void SetResultBlock(ResultType type)
    {
        ResultInfoBlock block = _resultDic[type];

        LabelModule mainLabel = FindUIObject<LabelModule>("MainTextLabel");
        LabelModule subLabel = FindUIObject<LabelModule>("SubTextLabel");
        PanelObject icon = FindUIObject<PanelObject>("ResultIcon");

        mainLabel.SetText($"<dangle>{block.resultMainText}</>");
        subLabel.SetText($"<dangle>{block.resultSubText}</>");
        icon.Visual.sprite = block.icon;
    }

    public override void SceneUIEnd()
    {
    }

    public override void SceneUIStart()
    {
        if(_sceneAuido != null)
            SoundManager.Instance.PlayBGM(_sceneAuido);

        LabelModule backLabel = FindUIObject<LabelModule>("BackLabel");

        backLabel.OnClickEvent += (v) =>
        NetworkManager.Singleton.SceneManager.
        LoadScene("Lobby", UnityEngine.SceneManagement.LoadSceneMode.Additive);

        backLabel.OnHoverEvent += HandleBackLabelHover;
        backLabel.OnDesecendEvent += HandleBackLabelDescend;
    }

    private void HandleBackLabelDescend(UIObject obj)
    {
        LabelModule lm = obj as LabelModule;

        _backLabelSeq?.Kill();
        _backLabelSeq = DOTween.Sequence();

        _backLabelSeq.Append(lm.Text.DOColor(new Color(0.6705883f, 0.6627451f, 0.5411765f), 0.1f));
        _backLabelSeq.Join(lm.transform.DOScale(1f, 0.1f));
    }

    private void HandleBackLabelHover(UIObject obj)
    {
        LabelModule lm = obj as LabelModule;

        _backLabelSeq?.Kill();
        _backLabelSeq = DOTween.Sequence();

        _backLabelSeq.Append(lm.Text.DOColor(Color.gray, 0.1f));
        _backLabelSeq.Join(lm.transform.DOScale(1.1f, 0.1f));
    }
}
