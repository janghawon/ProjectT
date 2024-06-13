using UIFunction;
using UnityEngine;

/*
* Class: ExtensionMono
* Author: ���Ͽ�
* Created: 2024�� 6�� 13�� �����
* Description: �߰����� �ý��ۿ� ���ϰ� �����ϰ� ���� �� ��ӹ޴� ���
*/

public abstract class ExtensionMono : MonoBehaviour
{
    public T FindUIObject<T>(int uiMask) where T : UIObject
    {
        SceneUIContent content = UIManager.Instance.CurrentSceneUiObject;
        return content.FindUIObject<T>(uiMask);
    }
}
