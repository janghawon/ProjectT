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
* Author: ���Ͽ�
* Created: 2024�� 6�� 21�� �ݿ���
* Description: ������ ���� 
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
