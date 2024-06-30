using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;
using UIFunction;
using System;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class GameControllSystem : ExtensionMono
{
    private Sequence _textOnPointerSeq;
    [SerializeField] private Color _hoverColor;
    [SerializeField] private Color _normalColor;

    [SerializeField] private UnityEvent<TextMeshProUGUI> _textHoverEvent;
    [SerializeField] private UnityEvent<TextMeshProUGUI> _textDecesendEvent;

    private void Start()
    {
        LabelModule titleLable = FindUIObject<LabelModule>("TitleLabel");
        titleLable.OnHoverEvent += (label) => _textHoverEvent?.Invoke(titleLable.Text);
        titleLable.OnDesecendEvent += (label) => _textDecesendEvent?.Invoke(titleLable.Text);

        LabelModule lable = FindUIObject<LabelModule>("GameStartLabel");

        lable.OnClickEvent += HandleGameStart;
        lable.OnHoverEvent += HandleTextHover;
        lable.OnDesecendEvent += HandleTextDescend;

        lable = FindUIObject<LabelModule>("GameQuitLabel");

        lable.OnHoverEvent += HandleTextHover;
        lable.OnDesecendEvent += HandleTextDescend;
        lable.OnClickEvent += (d) => Application.Quit();
    }

    private void HandleTextDescend(UIObject obj)
    {
        LabelModule label = obj as LabelModule;

        _textOnPointerSeq?.Kill();

        _textOnPointerSeq.Append(label.Text.DOColor(_normalColor, 0.2f));
        _textOnPointerSeq.Join(label.transform.DOScale(1f, 0.2f));
    }

    private void HandleTextHover(UIObject obj)
    {
        LabelModule label = obj as LabelModule;

        _textOnPointerSeq?.Kill();

        _textOnPointerSeq.Append(label.Text.DOColor(_hoverColor, 0.2f));
        _textOnPointerSeq.Join(label.transform.DOScale(1.1f, 0.2f));
    }

    private void HandleGameStart(UIObject obj)
    {
        NetworkManager.Singleton.SceneManager.LoadScene("Main_Save", LoadSceneMode.Additive);
    }
}
