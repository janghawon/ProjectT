using Extension;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.Events;

public class PurchaseSystem : ExtensionMono
{
    private LabelModule _purchaseLabel;
    private string _purchaseText;
    private ItemInfo _selectItemInfo;

    [SerializeField] private UnityEvent<ItemInfo> _onPurchaseItemEvent;

    private void Start()
    {
        AddSetupCallback(SetUpPurchaseBtn);
    }

    public void SetUpPurchaseBtn()
    {
        ButtonModule purchaseBtn = FindUIObject<ButtonModule>("PurchaseButton");
        _purchaseLabel = FindUIObject<LabelModule>("PurchaseLabel");
        _purchaseText = _purchaseLabel.TypedText;

        purchaseBtn.OnHoverEvent += HandleButtonHover;
        purchaseBtn.OnDesecendEvent += HandleButtonDesecend;
        purchaseBtn.OnClickEvent += HandleButtonClick;
    }

    private void HandleButtonHover(UIObject obj)
    {
        _purchaseLabel.Animator.referenceFontSize = 130;
        _purchaseLabel.SetText($"<bounce>{_purchaseText}</>");
    }

    private void HandleButtonDesecend(UIObject obj)
    {
        _purchaseLabel.Animator.referenceFontSize = 200;
        _purchaseLabel.SetText($"<wave>{_purchaseText}</>");
    }

    private void HandleButtonClick(UIObject obj)
    {
        _onPurchaseItemEvent?.Invoke(_selectItemInfo);
    }

    public void SetItemInfo(ItemInfo itemInfo)
    {
        _selectItemInfo = itemInfo;
    }
}
