using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFunction;
using UnityEngine.UI;
using System;

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


        SetResultBlock(ResultType.Defeat);
    }
}
