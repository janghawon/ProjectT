using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UIFunction
{
    // ���� �����ϴ� ��� UI�� �ڽ����� �δ� �θ� ���� �ִ� ��ũ��Ʈ
    public abstract class SceneUIContent : MonoBehaviour
    {
        public Action SceneUIStartAction { get; set; }

        // �������� �� Ÿ�� �Ҵ�
        [SerializeField] private SceneType _uiType;
        public SceneType UIType => _uiType;

        [SerializeField] protected AudioClip _sceneAuido;

        // �ڽ� �� �����ϴ� UI�� ���� ����Ʈ
        private List<UIObject> _childUIList = new List<UIObject>();

        // �ڽ� �� �����ϴ� UI�� Ư¡�� Enum Flag�� ��� �Ű������� �Ѱ��ָ�
        // �ش��ϴ� Ư¡�� ���� UI �� �������ִ� �޼���
        public T FindUIObject<T>(UIKeyword keywordMask) where T : UIObject
        {
            // linq������ ���� ������ ���ԵǴ� UI�� Ž��
            UIObject uiObject = 
            _childUIList.FirstOrDefault(x => (x.Keyword & keywordMask) == keywordMask);

            // ã�� ���Ѵٸ� Error �޼����� �Բ� �Լ� ���� ����
            if(uiObject == null)
            {
                Debug.LogError($"Cant Found UiObject : {typeof(T)} \nPlease Check Your Keword : {keywordMask}");
                return null;
            }

            // ã�� UI�� ���Ŀ� �°� ��ȯ �� ����
            return uiObject as T;
        }

        // �ڽ� �� �����ϴ� UI�� �̸��� ���� ã�� �������ִ� �޼���
        public T FindUIObject<T>(string objName) where T : UIObject
        {
            // linq ������ ����� UI�� �̸����� Ư�� UI�� ã�´�.
            UIObject uiObject =
            _childUIList.FirstOrDefault(x => x.gameObject.name == objName);

            // ã�� ���Ѵٸ� Error �޼����� �Բ� �Լ� ���� ����
            if (uiObject == null)
            {
                Debug.LogError($"Cant Found UiObject : {typeof(T)} \nPlease Check Object Name : {objName}");
                return null;
            }

            // ã�� UI�� ���Ŀ� �°� ��ȯ �� ����
            return uiObject as T;
        }

        // �ڽ� �� �����ϴ� UI�� Ư¡�� Enum Flag�� ��� �Ű������� �Ѱ��ָ�
        // �ش��ϴ� Ư¡�� ���� ��� UI �� �������ִ� �޼���
        public UIObject[] FindAllUIObject<T>(UIKeyword keywordMask) where T : UIObject
        {
            // linq ������ ����� �Ѱܹ��� Ư¡�� �ش��ϴ� ��� UI�� ã�´�.
            UIObject[] uiObjetArr =
            _childUIList.FindAll(x => (x.Keyword & keywordMask) == keywordMask).ToArray();

            // ã�� UI���� �迭�� ���·� ����
            return uiObjetArr;
        }

        // ������ DFSŽ�� �˰����� ���� ��� �ڽ��� ��ȸ�ϸ� UI�� ã�� ����Ʈ�� �־��ִ� �޼���
        public void GenerateOnUIObject()
        {
            _childUIList.Clear();
            Stack<Transform> stack = new Stack<Transform>();

            // ���� ���� transform�� ���ÿ� �־��ش�.
            stack.Push(transform);

            while (stack.Count > 0)
            {
                // Ž���� trnasform�� stack���� ã�´�.
                Transform current = stack.Pop();

                // transform�� �ڽĵ��� ��ȸ�ϸ� UI�� ã�´�.
                foreach (Transform child in current)
                {
                    if(child.TryGetComponent<UIObject>(out UIObject ui))
                    {
                        _childUIList.Add(ui);
                    }
                    
                    // transform�� �ڽ��� stack�� �ִ´�.
                    stack.Push(child);
                }
            }
        }

        // �ش� UI �׷��� �����Ǿ��� �� ȣ��
        public abstract void SceneUIStart();

        // �ش� UI �׷��� �����Ǿ��� �� ȣ��
        public abstract void SceneUIEnd();
    }
}
