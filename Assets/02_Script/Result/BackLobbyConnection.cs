using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using Extension;
using System;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class BackLobbyConnection : ExtensionMono
{
    private void Start()
    {
        LabelModule backLabel = FindUIObject<LabelModule>("BackLabel");

        backLabel.OnHoverEvent += HandleFadeText;
        backLabel.OnDesecendEvent += HandleNormalingText;
        backLabel.OnClickEvent += HandleToChangeLobbyScene;
    }

    private void HandleToChangeLobbyScene(UIObject obj)
    {
        NetworkManager.Singleton.SceneManager.LoadScene("LobbyScene", LoadSceneMode.Additive);
    }

    private void HandleNormalingText(UIObject obj)
    {
        LabelModule label = obj as LabelModule;

        label.Text.DOKill();
        label.Text.DOFade(0.4f, 0.2f);
    }

    private void HandleFadeText(UIObject obj)
    {
        LabelModule label = obj as LabelModule;

        label.Text.DOKill();
        label.Text.DOFade(1, 0.2f);
    }
}
