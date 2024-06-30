using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
* Class: TarotCard
* Author: 장하원
* Created: 2024년 6월 20일 목요일
* Description: 타로 카드 
*/

public class TarotCard : UIObject
{
    private bool _onPointerThisCard;
    public bool OnPointerThisCard => _onPointerThisCard;

    public bool OnSelectThisCard { get; set; }

    [SerializeField] private int _wavingFontValue = 200;
    [SerializeField] private int _wiggleFontValue = 150;

    [SerializeField] private Image _tarotVisual;
    public Transform VisualTrm => _tarotVisual.transform;

    [SerializeField] private LabelModule _infoLabel;
    [SerializeField] private LabelModule _nameLabel;

    private string _saveNameText;
    private string _saveInfoText;

    public TarotCardInfo Info { get; private set; }

    public void SetInfo(TarotCardInfo info)
    {
        _tarotVisual.sprite = info.visual;
        Info = info;
    }

    public void SetLabelText(string info, string cardname)
    {
        _saveInfoText = info;
        _saveNameText = cardname;

        NormaingLabelText(null);
    }

    public void EmphasizeLabelText(UIObject obj)
    {
        _infoLabel.Animator.referenceFontSize = 150;
        _infoLabel.SetText($"<wiggle>{_saveInfoText}</>");

        _nameLabel.Animator.referenceFontSize = 150;
        _nameLabel.SetText($"<wiggle>{_saveNameText}</>");

        _infoLabel.Animator.typewriterStartsAutomatically = false;
        _nameLabel.Animator.typewriterStartsAutomatically = false;
    }

    public void NormaingLabelText(UIObject obj)
    {
        _infoLabel.Animator.referenceFontSize = 200;
        _infoLabel.SetText($"<wave>{_saveInfoText}</>");

        _nameLabel.Animator.referenceFontSize = 200;
        _nameLabel.SetText($"<bounce>{_saveNameText}</>");
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (OnSelectThisCard) return;

        OnSelectThisCard = true;

        base.OnPointerClick(eventData);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        _onPointerThisCard = true;

        base.OnPointerEnter(eventData);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        _onPointerThisCard = false;

        base.OnPointerExit(eventData);
    }

}
