using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UIFunction;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class GamePlayManager : NetworkMonoSingleton<GamePlayManager>
{

    [SerializeField] private ItemInfo _debugItem;
    [SerializeField] private List<ItemInfo> _items;
    [SerializeField] private Table _enemyTable;
    [SerializeField] private Table _table;
    [SerializeField] private List<GameObject> _disablePanel;

    private List<ItemInstance> _enemyInstanceList = new();

    public ulong EnemyClientId { get; private set; }
    public bool IsUsingStore { get; private set; }

    public void StartGamePass()
    {

        StartPassClientRPC();

    }

    [ClientRpc]
    private void StartPassClientRPC()
    {

        EnemyClientId = NetworkManager.ConnectedClientsIds.FirstOrDefault(x => x != NetworkManager.LocalClientId);

        StartCoroutine(StartPass());

    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnLinkItemServerRPC(FixedString128Bytes name, FixedString128Bytes guid, ulong targetClientRPC)
    {

        SpawnLinkItemClientRPC(name, guid, targetClientRPC.GetRpcParams());

    }

    [ClientRpc]
    private void SpawnLinkItemClientRPC(FixedString128Bytes name, FixedString128Bytes guid, ClientRpcParams @params)
    {

        var obj = _items.Find(x => x.itemName == name.ToString());
        _enemyTable.SpawnItem(obj.prefab, Guid.Parse(guid.ToString()), out var ins);
        _enemyInstanceList.Add(ins);

    }

    [ServerRpc(RequireOwnership = false)]
    public void LinkUseItemServerRPC(FixedString128Bytes guid, ulong tergetClientId)
    {

        LinkUseItemClientRPC(guid, tergetClientId.GetRpcParams());

    }

    [ClientRpc]
    private void LinkUseItemClientRPC(FixedString128Bytes guid, ClientRpcParams @params)
    {

        var obj = _enemyInstanceList.Find(x => x.GUID == Guid.Parse(guid.ToString()));

        Debug.Log(obj);

        if(obj != null)
        {

            _enemyInstanceList.Remove(obj);
            obj.UseLink();

        }


    }

    [ServerRpc(RequireOwnership = false)]
    public void OpenItemServerRPC()
    {

        OpenItemClientRPC();

    }

    [ClientRpc]
    public void OpenItemClientRPC()
    {

        foreach(var item in _disablePanel)
        {

            item.gameObject.SetActive(false);

        }

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

            OpenItemServerRPC();

        }

    }

    public void BuyItem(ItemInfo info)
    {

        if (PlayerDataManager.Instance.Data.gold < info.price) return;

        if (SpawnItem(info))
        {

            PlayerDataManager.Instance.AddGold(-info.price);

        }

    }

    public bool SpawnItem(ItemInfo info)
    {

        var id = Guid.NewGuid();
        bool b = _table.SpawnItem(info.prefab, id, out var _);

        if (b)
        {

            SpawnLinkItemServerRPC(info.itemName, id.ToString(), EnemyClientId);

        }

        return b;
    }
    public void AllClientGetItem(ItemInfo itemInfo)
    {
        SpawnItem(itemInfo);
    }

    //[ClientRpc]
    //private void AllClientGetItemClientRpc(string itemName)
    //{
    //    _table.SpawnItem(ItemManager.Instance.GetItem(itemName).prefab, out var _);
    //}

    public void GetRandomItem()
    {
        GetRandomItemClientRpc();
    }

    [ClientRpc]
    private void GetRandomItemClientRpc()
    {
        var item = ItemManager.Instance.GetRandomItem();

        if(SpawnItem(item))
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

    public void CheckEndGame()
    {

        var data = PlayerDataManager.Instance.Data;
        var enemyData = PlayerDataManager.Instance[EnemyClientId];

        var h = data.health > enemyData.health ? enemyData.clientId : data.clientId;

        PlayerDie(h);

    }

}
