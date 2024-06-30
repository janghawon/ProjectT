using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemPicker : MonoBehaviour
{
    [SerializeField] private CanvasGroup _visualGroup;

    [SerializeField] private Image _itemProfile;
    [SerializeField] private LabelModule _itemNameLabel;
    [SerializeField] private LabelModule _itemInfoLabel;

    private StoreItemElement _currentItemElement;

    public void SetInfo(StoreItemElement element, ItemInfo info)
    {

        if(_currentItemElement != null)
        {
            _currentItemElement.OutSelecting();
        }

        //Debug
        GamePlayManager.Instance?.BuyItem(info);
        //Debug

        _currentItemElement = element;

        //_itemProfile.sprite = info.visual;
        _itemNameLabel.SetText($"<wiggle>{info.itemName}</>");
        _itemInfoLabel.SetText($"<wave>{info.info}</>");

        if (_visualGroup.alpha == 1) return;

        _visualGroup.interactable = true;
        _visualGroup.DOFade(1, 0.2f);
    }

    public void OutSelecting()
    {
        _currentItemElement?.OutSelecting();
        _visualGroup.interactable = false;
        _visualGroup.alpha = 0;
    }
}
