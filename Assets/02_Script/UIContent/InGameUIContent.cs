using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public class InGameUIContent : SceneUIContent
{
    [SerializeField] private UnityEvent _setUpEvent;

    public override void SceneUIEnd()
    {
    }

    public override void SceneUIStart()
    {
        _setUpEvent?.Invoke();
    }
}
