using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UIFunction;

public class ResultScene : MonoBehaviour
{

    private IEnumerator Start()
    {

        yield return null;

        GameManager.Instance.UnLoadScene("Game");
        UIManager.Instance.ChangeSceneUIOnChangeScene(SceneType.Result);

        yield return null;

        GameResultUI ui = UIManager.Instance.GetSceneUIContent<GameResultUI>();

        if (PlayerPrefs.GetInt("DIE_PLAYER", -1) == (int)NetworkManager.Singleton.LocalClientId)
        {
            ui.SetResultBlock(ResultType.Defeat);
        }
        else
        {
            ui.SetResultBlock(ResultType.Win);
        }
    }

}
