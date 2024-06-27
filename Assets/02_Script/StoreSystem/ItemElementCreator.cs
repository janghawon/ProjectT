using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public class ItemElementCreator : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private StoreItemElement _storeItemPrefab;
    [SerializeField] private Transform _itemElementContent;
    private StoreItemElement[] _storeItemElementArr;

    [Header("Container")]
    [SerializeField] private ItemInfo[] _itemInfo;

    [Header("Event")]
    [SerializeField] private UnityEvent<StoreItemElement, ItemInfo> _selectItemEvent;

    private void Start()
    {
        CreateItem();
    }

    public void CreateItem()
    {
        _storeItemElementArr = new StoreItemElement[_itemInfo.Length];

        for(int i = 0; i < _itemInfo.Length; i++)
        {
            ItemInfo info = _itemInfo[i];

            var sie = Instantiate(_storeItemPrefab, _itemElementContent);
            sie.SetInfo(info);

            sie.OnHoverEvent += HandleHoverAction;
            sie.OnDesecendEvent += HandleDescentAction;

            sie.OnClickEvent += HandleOnSelecting;
            sie.OnSelectThisItem += () => _selectItemEvent?.Invoke(sie, info);

            _storeItemElementArr[i] = sie;
        }
    }

    private void HandleOnSelecting(UIObject obj)
    {
        var sie = obj as StoreItemElement;
        if (sie.OnSelecting) return;

        sie.OnSelectThisItem?.Invoke();
        HandleDescentAction(obj);
        sie.InSelecting();
    }

    private void HandleDescentAction(UIObject obj)
    {
        var sie = obj as StoreItemElement;
        if (sie.OnSelecting) return;

        sie.transform.DOKill();
        sie.transform.DOScale(Vector3.one, 0.2f);
    }

    private void HandleHoverAction(UIObject obj)
    {
        var sie = obj as StoreItemElement;
        if (sie.OnSelecting) return;

        sie.transform.DOKill();
        sie.transform.DOScale(Vector3.one * 1.03f, 0.2f).SetEase(Ease.OutBack);
        
    }
}
