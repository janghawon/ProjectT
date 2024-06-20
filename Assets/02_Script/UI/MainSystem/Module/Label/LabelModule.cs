using Extension;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/*
* Class: LabelModule
* Author: 장하원
* Created: 2024년 6월 19일 수요일
* Description: 간단한 라벨 텍스트 모듈
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

