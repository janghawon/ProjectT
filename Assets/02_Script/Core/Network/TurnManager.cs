using Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;

public sealed class TurnManager : NetworkMonoSingleton<TurnManager>, INetworkInitable
{

    /// <summary>
    /// 턴의 총 시간
    /// </summary>
    [SerializeField] private int _turnTime;

    /// <summary>
    /// 적용되는 턴의 총 시간
    /// </summary>
    private int _applyTurnTime;

    /// <summary>
    /// 현재 턴의 시간
    /// </summary>
    private NetworkVariable<int> _currentTurnTime = new NetworkVariable<int>(
            default(int),
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server);

    /// <summary>
    /// 현재 턴인 클라 Id
    /// </summary>
    private NetworkVariable<ulong> _turnPlayerId = new NetworkVariable<ulong>(
            default(ulong),
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server);

    private List<ulong> _connectClientIds = new();

    public int CurrentTurnTime => _currentTurnTime.Value;
    public ulong TurnPlayerId => _turnPlayerId.Value;

    public event TurnChange OnTurnChanged;
    public event TimeChange OnTimeChanged;

    public override void OnNetworkSpawn()
    {

        _turnPlayerId.OnValueChanged += HandleTurnValueChanged;
        _currentTurnTime.OnValueChanged += HandleTimeValueChanged;
        _connectClientIds = NetworkManager.ConnectedClientsIds.ToList();

    }

    private void HandleTimeValueChanged(int previousValue, int newValue)
    {

        OnTimeChanged?.Invoke(previousValue, newValue);

    }

    private void HandleTurnValueChanged(ulong previousValue, ulong newValue)
    {

        OnTurnChanged?.Invoke(previousValue, newValue);

    }

    public void Init()
    {

        if (IsServer)
        {

            StartTurnLogic();
            _applyTurnTime = _turnTime;

        }

    }

    private void StartTurnLogic()
    {

        _turnPlayerId.Value = GetRandomPlayerId();
        StartCoroutine(TimePassCo());

    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ulong GetRandomPlayerId()
    {

        return _connectClientIds.Shuffle()[0];

    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ChangeTurn()
    {

        _turnPlayerId.Value = _connectClientIds.Find(x => x != _turnPlayerId.Value);

    }

    private IEnumerator TimePassCo()
    {

        var wait = new WaitForSecondsRealtime(1);

        while (true)
        {

            var apply = _currentTurnTime.Value = _applyTurnTime;

            for(int i = 0; i < apply; i++)
            {

                yield return wait;
                _currentTurnTime.Value -= 1;

            }

            yield return wait;

            ChangeTurn();

            yield return wait;

        }

    }

    public override void Dispose()
    {

        base.Dispose();

    }

    /// <summary>
    /// 턴 바뀜
    /// </summary>
    /// <param name="oldId">이전 Id</param>
    /// <param name="newId">새로운 Id</param>
    public delegate void TurnChange(ulong oldId, ulong newId);

    /// <summary>
    /// 시간 바뀜
    /// </summary>
    /// <param name="oldTime">이전 시간</param>
    /// <param name="newTime">새로운 시간</param>
    public delegate void TimeChange(int oldTime, int newTime);

}