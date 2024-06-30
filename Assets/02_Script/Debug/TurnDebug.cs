using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TurnDebug : MonoBehaviour
{

    private void Start()
    {

        TurnManager.Instance.OnTimeChanged += HandleTimeChanged;
        TurnManager.Instance.OnTurnChanged += HandleTurnChanged;

    }

    private void HandleTurnChanged(ulong oldId, ulong newId)
    {

        Debug.Log($"�� �ٲ� : {newId}");

    }

    private void HandleTimeChanged(int oldTime, int newTime)
    {

        Debug.Log($"�ð� �帧 : {newTime}");

    }

}
