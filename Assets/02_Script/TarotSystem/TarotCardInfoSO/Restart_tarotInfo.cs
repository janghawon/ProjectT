using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TarotCardInfo/Restart")]
public class Restart_tarotInfo : TarotCardInfo
{
    public override void ApplyTarotEffect()
    {
        ulong clientID = GamePlayManager.Instance.EnemyClientId;
        ulong hostID = NetworkManager.Singleton.LocalClientId;

        int maxHealth = PlayerDataManager.Instance.MaxHealth;

        PlayerDataManager.Instance.SetHealth(clientID, maxHealth);
        PlayerDataManager.Instance.SetHealth(hostID, maxHealth);
    }
}
