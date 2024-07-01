using System;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public enum InGameType
{
    tarotApear,
    sotre,
    mainText,
    destination,
    none
}

public class InGameUIContent : SceneUIContent
{
    [SerializeField] private UnityEvent _setUpEvent;
    [SerializeField] private GameObject[] _inGameContentArr;
    [SerializeField] private AudioClip _pageClip;
    [SerializeField] private GameObject _currentContent;

    [SerializeField] private MainTextSetter _textSetter;

    public void EnableContent(InGameType type)
    {
        if(type != InGameType.none)
        {
            SoundManager.Instance.PlaySFX(_pageClip);

            if(_currentContent != null)
            {

                _currentContent.SetActive(false);

            }

            _currentContent = _inGameContentArr[(int)type];
            _currentContent.SetActive(true);
        }
        else
        {
            _currentContent.SetActive(false);
        }
    }

    public override void SceneUIEnd()
    {
    }

    public override void SceneUIStart()
    {
        if (_sceneAuido != null)
            SoundManager.Instance.PlayBGM(_sceneAuido);

        _setUpEvent?.Invoke();

        TurnManager.Instance.OnTurnChanged += HandleTurnChanged;
    }

    private void HandleTurnChanged(ulong oldId, ulong newId)
    {

        //if (TurnManager.Instance.IsGoldTime) return;

        EnableContent(InGameType.mainText);
        
        if(TurnManager.Instance.MyTurn)
        {
            _textSetter.SetMainText("자유행동", "60초 동안 행동 할 수 있습니다.");
        }
        else
        {
            _textSetter.SetMainText("상대차례", "상대의 전략을 간파하세요.");
        }

        StartCoroutine(FadeTextCo());
    }

    private IEnumerator FadeTextCo()
    {
        _textSetter.FadeText(1, 0.2f);
        yield return new WaitForSeconds(1.3f);
        _textSetter.FadeText(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        EnableContent(InGameType.sotre);
    }
}
