using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{

    private IEnumerator Start()
    {

        yield return null;

        if (NetworkManager.Singleton.IsServer)
        {

            GameManager.Instance.UnLoadScene("Lobby");
            GamePlayManager.Instance.StartGamePass();

        }

    }

}
