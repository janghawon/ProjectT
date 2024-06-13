using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

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

        public Action OnClickEvent { get; set; }
        public Action OnHoverEvent { get; set; }
        public Action OnDesecendEvent { get; set; }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverEvent?.Invoke();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            OnDesecendEvent?.Invoke();
        }
    }
}

