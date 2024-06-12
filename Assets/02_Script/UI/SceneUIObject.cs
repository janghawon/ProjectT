using UnityEngine;
using System.Collections.Generic;

namespace UI
{
    public abstract class SceneUIObject : MonoBehaviour
    {
        [SerializeField] private SceneType _uiType;
        public SceneType UIType => _uiType;

        private List<Transform> _childList = new List<Transform>();

        public void GenerateAllChildren()
        {
            Stack<Transform> stack = new Stack<Transform>();

            stack.Push(transform);

            while (stack.Count > 0)
            {
                Transform current = stack.Pop();
                foreach (Transform child in current)
                {
                    _childList.Add(child);
                    stack.Push(child);
                }
            }
        }

        public abstract void SceneUIStart();
        public abstract void SceneUIEnd();
    }
}
