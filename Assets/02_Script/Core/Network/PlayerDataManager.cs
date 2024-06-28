using System;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AlcoholState
{

    Safe = 0,
    NotSafe = 1,
    VeryNotSafe = 2

}

public struct PlayerData : INetworkSerializable, IEquatable<PlayerData>
{

    public int health;
    public int gold;
    public AlcoholState state;
    public ulong clientId;

    public bool Equals(PlayerData other)
    {

        return other.clientId == clientId;

    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {

        serializer.SerializeValue(ref health);
        serializer.SerializeValue(ref gold);
        serializer.SerializeValue(ref state);
        serializer.SerializeValue(ref clientId);

    }

}

public class PlayerDataManager : NetworkMonoSingleton<PlayerDataManager>, INetworkInitable
{

    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _startGold = 20;

    private NetworkList<PlayerData> _playerDatas = new NetworkList<PlayerData>();

    public PlayerData Data => this[NetworkManager.LocalClientId];
    public event PlayerDataChange OnPlayerDataChanged;

    public PlayerData this[ulong id]
    {

        get
        {

            PlayerData data = default;

            foreach (var item in _playerDatas)
            {

                if (id == item.clientId)
                    data = item;

            }

            return data;

        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void AddGoldServerRPC(ulong targetClientId, int gold)
    {

        AddGold(targetClientId, gold);

    }

    public void AddGold(int gold)
    {

        AddGoldServerRPC(NetworkManager.LocalClientId, gold);

    }

    public void AddGold(ulong targetClientId, int gold)
    {

        var data = this[targetClientId];
        var idx = FindIndex(data);

        data.gold += gold;

        _playerDatas[idx] = data;

    }

    [ServerRpc(RequireOwnership = false)]
    private void AddHealthServerRPC(ulong targetClientId, int health)
    {

        AddHealth(targetClientId, health);

    }

    public void AddHealth(int health)
    {

        AddHealthServerRPC(NetworkManager.LocalClientId, health);

    }

    public void AddHealth(ulong targetClientId, int health)
    {

        var data = this[targetClientId];
        var idx = FindIndex(data);

        data.health += health;
        data.health = Mathf.Clamp(data.health, 0, _maxHealth);

        Debug.Log($"√§∑¬ πŸ≤Ò : id / {targetClientId} , health / {data.health}");

        if(data.health == 0)
        {

            GamePlayManager.Instance.PlayerDie(data.clientId);

        }

        _playerDatas[idx] = data;

    }

    [ServerRpc(RequireOwnership = false)]
    private void SetAlcoholServerRPC(ulong targetClientId, AlcoholState state)
    {

        SetAlcohol(targetClientId, state);

    }

    public void SetAlcohol(AlcoholState state)
    {

        SetAlcoholServerRPC(NetworkManager.LocalClientId, state);

    }

    public void SetAlcohol(ulong targetClientId, AlcoholState state)
    {

        if (IsServer)
        {

            var data = this[targetClientId];
            var idx = FindIndex(data);

            data.state = state;

            _playerDatas[idx] = data;

        }
        else
        {

            SetAlcoholServerRPC(targetClientId, state);

        }


    }

    public void Init()
    {

        _playerDatas.OnListChanged += HandleDataChanged;
        TurnManager.Instance.OnTurnChanged += HandleTurnChanged;

        if (IsServer)
        {

            foreach (var item in NetworkManager.ConnectedClientsIds)
            {

                _playerDatas.Add(CreatePlayerData(item));

            }

        }

    }

    private void HandleTurnChanged(ulong oldId, ulong newId)
    {

        if (IsServer)
        {

            AddGold(newId, 10);
            SetAlcohol(newId, (AlcoholState)Random.Range(0, 2));

        }

    }

    private void HandleDataChanged(NetworkListEvent<PlayerData> changeEvent)
    {

        OnPlayerDataChanged?.Invoke(changeEvent.Value);

    }

    private int FindIndex(PlayerData data)
    {

        for (int i = 0; i < _playerDatas.Count; i++)
        {

            if (_playerDatas[i].Equals(data))
            {

                return i;

            }

        }

        return -1;

    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PlayerData CreatePlayerData(ulong clientId)
    {

        return new PlayerData
        {

            health = _maxHealth,
            gold = _startGold,
            state = (AlcoholState)Random.Range(0, 2),
            clientId = clientId,

        };

    }

    /// <summary>
    /// µ•¿Ã≈Õ πŸ≤Ò
    /// </summary>
    /// <param name="changeData"></param>
    public delegate void PlayerDataChange(PlayerData changeData);

}
