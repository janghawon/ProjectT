using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TarotCardInfo/PierceTheHeart")]
public class PierceTheHeart_TarotInfo : TarotCardInfo
{
    public override void ApplyTarotEffect()
    {
        ulong clientID = GamePlayManager.Instance.EnemyClientId;
        ulong hostID = NetworkManager.Singleton.LocalClientId;

        int clientHealth = PlayerDataManager.Instance.GetHealth(clientID) - 1;
        int hostHealth = PlayerDataManager.Instance.GetHealth(hostID) - 1;

        PlayerDataManager.Instance.SetHealth(clientID, clientHealth);   
        PlayerDataManager.Instance.SetHealth(hostID, hostHealth);
    }
}
