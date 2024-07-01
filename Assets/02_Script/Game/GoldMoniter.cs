using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GoldMoniter : MonoBehaviour
{


    [SerializeField] private TMP_Text _text;

    private void Awake()
    {

        PlayerDataManager.Instance.OnPlayerDataChanged += HandleDataChanged;

    }

    private void HandleDataChanged(PlayerData changeData)
    {

        if(changeData.clientId == NetworkManager.Singleton.LocalClientId)
        {

            _text.text = $"Gold : {changeData.gold}";

        }


    }
}
