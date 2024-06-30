using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TarotCardInfo/BigChanceInfo")]
public class BigReversal_TarotInfo : TarotCardInfo
{
    public override void ApplyTarotEffect()
    {
        SwapHealthServerRpc();
    }

    [ServerRpc]
    private void SwapHealthServerRpc()
    {
        ulong clientID = GamePlayManager.Instance.EnemyClientId;
        ulong hostID = NetworkManager.Singleton.LocalClientId;

        int clientHealth = PlayerDataManager.Instance.GetHealth(clientID);
        int hostHealth = PlayerDataManager.Instance.GetHealth(hostID);

        PlayerDataManager.Instance.SetHealth(clientID, hostHealth);
        PlayerDataManager.Instance.SetHealth(hostID, clientHealth);
    }
}
