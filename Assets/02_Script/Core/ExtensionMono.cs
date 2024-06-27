using System;
using UIFunction;
using UnityEngine;

/*
* Class: ExtensionMono
* Author: ���Ͽ�
* Created: 2024�� 6�� 13�� �����
* Description: �߰����� �ý��ۿ� ���ϰ� �����ϰ� ���� �� ��ӹ޴� ���
*/

namespace Extension
{
    // MonoBehaviour�� ��ӹ޾� ���ϰ� ��ɿ� ������ �� �ֵ��� �����ִ� Ȯ�� MonoBehaviour
    public abstract class ExtensionMono : MonoBehaviour
    {
        public void AddSetupCallback(Action callBack)
        {
            SceneUIContent content = UIManager.Instance.CurrentSceneUiObject;
            content.SceneUIStartAction += callBack;
        }

        public T FindUIObject<T>(UIKeyword uiMask) where T : UIObject
        {
            SceneUIContent content = UIManager.Instance.CurrentSceneUiObject;
            return content.FindUIObject<T>(uiMask);
        }

        public T FindUIObject<T>(string objectName) where T : UIObject
        {
            SceneUIContent content = UIManager.Instance.CurrentSceneUiObject;
            return content.FindUIObject<T>(objectName);
        }

        public UIObject[] FindAllUIObject<T>(UIKeyword uiMask) where T : UIObject
        {
            SceneUIContent content = UIManager.Instance.CurrentSceneUiObject;
            return content.FindAllUIObject<T>(uiMask);
        }
    }
}

