using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbyScene : NetworkBehaviour
{

    private int _cnt;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        JoinServerRPC();

        if (IsServer)
        {

            FindObjectOfType<LoadingUIContent>().SetJoinCodeLabel(HostSingle.Instance.GameManager.JoinCode);
            StartCoroutine(WaitJoinCo());

        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void JoinServerRPC()
    {

        _cnt++;

    }

    private IEnumerator WaitJoinCo()
    {

        yield return new WaitUntil(() => _cnt == 2);

        yield return new WaitForSeconds(1);

        GameManager.Instance.LoadScene("Game");

    }

}
