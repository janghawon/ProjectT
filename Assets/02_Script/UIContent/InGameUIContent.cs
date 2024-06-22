using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public class InGameUIContent : SceneUIContent
{
    [SerializeField] private UnityEvent<ButtonObject> _exitBtnSetUpEvent;

    public override void SceneUIEnd()
    {
    }

    public override void SceneUIStart()
    {
        int exitButtonMask = 
        UIManager.Instance.GetUIKewordMask(UIKeyword.Button, UIKeyword.Panel, UIKeyword.Exit);

        _exitBtnSetUpEvent?.Invoke(FindUIObject<ButtonObject>(exitButtonMask));
    }
}
