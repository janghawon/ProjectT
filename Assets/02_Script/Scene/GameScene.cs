using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{


    private IEnumerator Start()
    {

        if (NetworkManager.Singleton.IsServer)
        {

            GameManager.Instance.UnLoadScene("Lobby");

        }

        yield return null;

        UIManager.Instance.ChangeSceneUIOnChangeScene(UIFunction.SceneType.InGame);
        GamePlayManager.Instance.GetUI();

        if (NetworkManager.Singleton.IsServer)
        {

            GamePlayManager.Instance.StartGamePass();

        }

    }

}
