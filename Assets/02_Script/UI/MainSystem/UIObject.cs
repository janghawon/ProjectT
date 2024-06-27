using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using Extension;

/*
* Class: UIObject
* Author: ���Ͽ�
* Created: 2024�� 6�� 13�� �����
* Description: ��� UI���� �־��ִ� Ŭ����, 
*              Ŭ��, ȣ��, �𼾵� �� �̺�Ʈ ����
*/

namespace UIFunction
{
    public class UIObject : ExtensionMono,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private UIKeyword _myKeword;
        public UIKeyword Keyword => _myKeword;

        private Image _visual;
        public Image Visual
        {
            get
            {
                if(_visual == null)
                {
                    _visual = GetComponent<Image>();
                }
                return _visual;
            }
        }

        public Action<UIObject> OnClickEvent { get; set; }
        public Action<UIObject> OnHoverEvent { get; set; }
        public Action<UIObject> OnDesecendEvent { get; set; }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(this);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverEvent?.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            OnDesecendEvent?.Invoke(this);
        }
    }
}

