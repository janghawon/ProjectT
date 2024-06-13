using UnityEngine;
using UnityEngine.EventSystems;
using System;

/*
* Class: UIObject
* Author: 장하원
* Created: 2024년 6월 13일 목요일
* Description: 모든 UI에게 넣어주는 클래스, 
*              클릭, 호버, 디센드 시 이벤트 관리
*/

namespace UIFunction
{
    public abstract class UIObject : ExtensionMono, 
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private UIKeyword _myKeword;
        public UIKeyword Keyword => _myKeword;

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

