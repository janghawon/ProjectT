using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;
using UIFunction;

public class LobbyUIContent : SceneUIContent
{
    public override void SceneUIEnd()
    {
    }

    public override void SceneUIStart()
    {
        if (_sceneAuido != null)
            SoundManager.Instance.PlayBGM(_sceneAuido);
    }
}
