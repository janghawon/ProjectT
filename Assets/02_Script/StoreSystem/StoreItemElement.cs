using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFunction;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

/*
* Class: StoreItemElement
* Author: 장하원
* Created: 2024년 6월 21일 금요일
* Description: 아이템 정보 
*/

public class StoreItemElement : UIObject
{
    [SerializeField] private LabelModule _itemNameLabel;
    [SerializeField] private LabelModule _itemPriceLabel;
    [SerializeField] private Image _itemProfile;
    [SerializeField] private GameObject _itemSelectingMask;
    public Action OnSelectThisItem { get; set; }

    private bool _onSelecting;
    public bool OnSelecting => _onSelecting;

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

    public void SetInfo(ItemInfo info)
    {
        _itemNameLabel.SetText(info.itemName);
        _itemPriceLabel.SetText($"<pend>{info.price}</>");
        _itemProfile.sprite = info.visual;
    }

    public void InSelecting()
    {
        _onSelecting = true;
        _itemSelectingMask.SetActive(true);
    }

    public void OutSelecting()
    {
        _onSelecting = false;
        _itemSelectingMask.SetActive(false);
    }
}
