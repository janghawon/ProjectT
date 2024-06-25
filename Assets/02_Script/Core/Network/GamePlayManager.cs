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
        StartCoroutine(StartPass());

    }

    [ClientRpc]
    private void StartPassClientRPC()
    {

        _table = FindObjectOfType<Table>();
        EnemyClientId = NetworkManager.ConnectedClientsIds.FirstOrDefault(x => x != NetworkManager.LocalClientId);

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

            Debug.Log("¾Æ±î¿î ³» µ·");
            //µ· ´Ù´Â ·ÎÁ÷

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
