using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UIFunction;

public class ResultScene : NetworkBehaviour
{

    private IEnumerator Start()
    {

        yield return null;

        GameManager.Instance.UnLoadScene("Game");
        UIManager.Instance.ChangeSceneUIOnChangeScene(SceneType.Result);

        yield return null;

        if (IsServer)
        {

            SetUIClientRPC(PlayerPrefs.GetInt("DIE_PLAYER", -1));

        }

    }

    [ClientRpc]
    public void SetUIClientRPC(int id)
    {
        GameResultUI ui = UIManager.Instance.GetSceneUIContent<GameResultUI>();

        if (id == (int)NetworkManager.Singleton.LocalClientId)
        {
            ui.SetResultBlock(ResultType.Defeat);
        }
        else
        {
            ui.SetResultBlock(ResultType.Win);
        }

    }

}
