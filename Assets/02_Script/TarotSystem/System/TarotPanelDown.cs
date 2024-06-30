using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotPanelDown : MonoBehaviour
{
    private void Start()
    {
        GamePlayManager.Instance.OnTarotClientDown += HandleTarotClinetDown;
    }

    private void HandleTarotClinetDown()
    {
        InGameUIContent content = UIManager.Instance.GetSceneUIContent<InGameUIContent>();

        content.EnableContent(InGameType.sotre);
    }
}
