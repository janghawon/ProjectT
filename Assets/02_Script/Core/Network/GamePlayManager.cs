using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UIFunction;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class GamePlayManager : NetworkMonoSingleton<GamePlayManager>
{

    [SerializeField] private ItemInfo _debugItem;
    private Table _table;

    public ulong EnemyClientId { get; private set; }
    public bool IsUsingStore { get; private set; }

    public void StartGamePass()
    {

        StartPassClientRPC();

    }

    [ClientRpc]
    private void StartPassClientRPC()
    {

        _table = FindObjectOfType<Table>();
        EnemyClientId = NetworkManager.ConnectedClientsIds.FirstOrDefault(x => x != NetworkManager.LocalClientId);

        StartCoroutine(StartPass());

    }

    public void GetUI()
    {

        FindObjectOfType<ActivationStore>().RegisterCallback(HandleActivationStore, HandleDisableStore);

    }

    private void HandleDisableStore()
    {

        IsUsingStore = false;

    }

    private void HandleActivationStore(UIObject @object)
    {

        IsUsingStore = true;

    }

    private void Update()
    {

        //Debug
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            BuyItem(_debugItem);

        }

    }

    public void BuyItem(ItemInfo info)
    {

        if (_table.SpawnItem(info.prefab))
        {

            PlayerDataManager.Instance.AddGold(-info.price);

        }

    }

    private IEnumerator StartPass()
    {

        yield return null;
        var array = FindObjectsOfType<MonoBehaviour>().OfType<INetworkInitable>();

        foreach(var obj in array)
        {

            obj.Init();

        }

    }

    public void PlayerDie(ulong diePlayerId)
    {

        PlayerPrefs.SetInt("DIE_PLAYER", (int)diePlayerId);
        GameManager.Instance.LoadScene("Result");

    }

}
