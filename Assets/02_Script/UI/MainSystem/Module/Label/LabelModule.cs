using Extension;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/*
* Class: LabelModule
* Author: ���Ͽ�
* Created: 2024�� 6�� 19�� ������
* Description: ������ �� �ؽ�Ʈ ���
*/

namespace UIFunction
{
    public class LabelModule : UIObject
    {
        [SerializeField] private TextMeshProUGUI _text;
        public TextMeshProUGUI Text => _text;

        public void SetText(string text)
        {
            Text.text = text;
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

