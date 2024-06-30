using UnityEngine;
using UIFunction;

/*
* Class: TitleUIContent
* Author: 장하원
* Created: 2024년 6월 13일 목요일
* Description: 타이틀 UI컨텐츠 관리
*/

public class TitleUIContent : SceneUIContent
{
    public override void SceneUIStart()
    {
        if (_sceneAuido != null)
            SoundManager.Instance.PlayBGM(_sceneAuido);
    }

    public override void SceneUIEnd()
    {

    }
}
