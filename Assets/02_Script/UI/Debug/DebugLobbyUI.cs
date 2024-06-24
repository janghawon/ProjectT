using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugLobbyUI : MonoBehaviour
{

    [SerializeField] private TMP_InputField _input;

    public async void CreateRoom()
    {

        bool v = await AppController.Instance.StartHostAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        if (v)
        {

            NetworkManager.Singleton.SceneManager.LoadScene("Game", LoadSceneMode.Additive);

        }

    }

    public void JoinRoom()
    {

        AppController.Instance.StartClientAsync(Guid.NewGuid().ToString(), _input.text);

    }
 
}
