using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RoomCreateOrJoiner : MonoBehaviour
{
    
    public async void CreateRoom(string roomName, string playerName)
    {

        await AppController.Instance.StartHostAsync(roomName, playerName);

        NetworkManager.Singleton.SceneManager.LoadScene("Lobby", UnityEngine.SceneManagement.LoadSceneMode.Additive);

    }

    public async void JoinRoom(string joinCode, string playerName)
    {

        await AppController.Instance.StartClientAsync(playerName, joinCode);

    }

}
