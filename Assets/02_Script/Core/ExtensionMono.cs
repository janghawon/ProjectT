using UIFunction;
using UnityEngine;

/*
* Class: ExtensionMono
* Author: 장하원
* Created: 2024년 6월 13일 목요일
* Description: 추가적인 시스템에 편하게 접근하고 싶을 때 상속받는 모노
*/

public abstract class ExtensionMono : MonoBehaviour
{
    public T FindUIObject<T>(int uiMask) where T : UIObject
    {
        SceneUIContent content = UIManager.Instance.CurrentSceneUiObject;
        return content.FindUIObject<T>(uiMask);
    }
}
