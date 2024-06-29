using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class GamePlayManager : NetworkMonoSingleton<GamePlayManager>
{

    [SerializeField] private ItemInfo _debugItem;
    private Table _table;

    public ulong EnemyClientId { get; private set; }

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

    public void AllClientGetItem(ItemInfo itemInfo)
    {
        AllClientGetItemClientRpc(itemInfo.itemName);
    }

    [ClientRpc]
    private void AllClientGetItemClientRpc(string itemName)
    {
        _table.SpawnItem(ItemManager.Instance.GetItem(itemName).prefab);
    }

    public void GetRandomItem()
    {
        GetRandomItemClientRpc();
    }

    [ClientRpc]
    private void GetRandomItemClientRpc()
    {
        var item = ItemManager.Instance.GetRandomItem();

        if(_table.SpawnItem(item.prefab))
        {
            Debug.Log($"Sucessful SpawnItem : {item.itemName}");
        }
        else
        {
            Debug.Log($"Failure SpawnItem : {item.itemName}");
        }
    }

    private IEnumerator StartPass()
    {

        yield return new WaitForSeconds(1);

        var array = FindObjectsOfType<MonoBehaviour>().OfType<INetworkInitable>();

        foreach(var obj in array)
        {

            obj.Init();

        }

    }
}
