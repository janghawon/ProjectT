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

        PlayerDataManager.Instance.CreateData();
        UIManager.Instance.ChangeSceneUIOnChangeScene(UIFunction.SceneType.InGame);
        

        if (NetworkManager.Singleton.IsServer)
        {

            //GamePlayManager.Instance.StartGamePass();

        }

    }

}
