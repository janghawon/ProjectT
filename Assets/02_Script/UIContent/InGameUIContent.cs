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
    destination
}

public class InGameUIContent : SceneUIContent
{
    [SerializeField] private UnityEvent _setUpEvent;
    [SerializeField] private GameObject[] _inGameContentArr;

    private GameObject _currentContent;

    public void ActiveContent(InGameType type)
    {
        _currentContent.SetActive(false);
        _currentContent = _inGameContentArr[(int)type];
        _currentContent.SetActive(true);
    }

    public override void SceneUIEnd()
    {
    }

    public override void SceneUIStart()
    {
        _setUpEvent?.Invoke();
    }
}
