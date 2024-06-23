using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
* Class: SliderObject
* Author: ���Ͽ�
* Created: 2024�� 6�� 18�� ȭ����
* Description: �����ϰ� �����̴� ��� ����
*/

namespace UIFunction
{
    public class SliderObject : UIObject
    {
        private Slider _slider;
        public UnityAction<float> OnValueChange { get; set; }

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnValueChange);
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
        }
    }
}

