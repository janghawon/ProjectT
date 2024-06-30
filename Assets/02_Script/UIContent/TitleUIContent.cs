using UnityEngine;
using UIFunction;

/*
* Class: TitleUIContent
* Author: ���Ͽ�
* Created: 2024�� 6�� 13�� �����
* Description: Ÿ��Ʋ UI������ ����
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
