using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace UIFunction
{
    public abstract class SceneUIContent : MonoBehaviour
    {
        [SerializeField] private SceneType _uiType;
        public SceneType UIType => _uiType;

        private List<UIObject> _childUIList = new List<UIObject>();

        public T FindUIObject<T>(int keywordMask) where T : UIObject
        {
            UIKeyword checkKeyword = (UIKeyword)keywordMask;

            UIObject uiObject = 
            _childUIList.FirstOrDefault(x => (x.Keyword & checkKeyword) == checkKeyword);

            if(uiObject == null)
            {
                Debug.LogError($"Cant Found UiObject : {typeof(T)} \nPlease Check Your Keword : {checkKeyword}");
                return null;
            }

            return uiObject as T;
        }

        public UIObject[] FindAllUIObject<T>(int keywordMask) where T : UIObject
        {
            UIKeyword checkKeyword = (UIKeyword)keywordMask;

            UIObject[] uiObjetArr =
            _childUIList.FindAll(x => (x.Keyword & checkKeyword) == checkKeyword).ToArray();

            return uiObjetArr;
        }

        public void GenerateOnUIObject()
        {
            Stack<Transform> stack = new Stack<Transform>();

            stack.Push(transform);

            while (stack.Count > 0)
            {
                Transform current = stack.Pop();
                foreach (Transform child in current)
                {
                    if(child.TryGetComponent<UIObject>(out UIObject ui))
                    {
                        _childUIList.Add(ui);
                    }
                    
                    stack.Push(child);
                }
            }
        }
        public abstract void SceneUIStart();
        public abstract void SceneUIEnd();
    }
}
