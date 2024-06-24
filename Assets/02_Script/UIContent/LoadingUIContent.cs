using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;
using UIFunction;

/*
* Class: LoadingUIContent
* Author: 장하원
* Created: 2024년 6월 20일 목요일
* Description: 로딩 UI
*/

public class LoadingUIContent : SceneUIContent
{
    private LabelModule _loadingLabel;
    [SerializeField] private string[] _loadingTextArr;
    private int _loadingTextIdx = 0;

    public override void SceneUIStart()
    {
        _loadingLabel = FindUIObject<LabelModule>(UIManager.Instance.GetUIKewordMask(UIKeyword.Label, UIKeyword.Deco));

        _loadingTextArr.Shuffle();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _loadingLabel.SetText($"<wiggle>{_loadingTextArr[_loadingTextIdx]}</wiggle>");

            _loadingTextIdx++;

            if(_loadingTextIdx == _loadingTextArr.Length)
            {
                _loadingTextIdx = 0;
            }
        }
    }

    public override void SceneUIEnd()
    {

    }
}
