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
    /// ���� �� �ð�
    /// </summary>
    [SerializeField] private int _turnTime;

    /// <summary>
    /// ����Ǵ� ���� �� �ð�
    /// </summary>
    private int _applyTurnTime;

    /// <summary>
    /// ��ŵ ��?
    /// </summary>
    private bool _isSkipped;

    /// <summary>
    /// ���� ���� �ð�
    /// </summary>
    private NetworkVariable<int> _currentTurnTime = new NetworkVariable<int>(
            default(int),
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server);

    /// <summary>
    /// ���� ���� Ŭ�� Id
    /// </summary>
    private NetworkVariable<ulong> _turnPlayerId = new NetworkVariable<ulong>(
            ulong.MaxValue,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server);

    private List<ulong> _connectClientIds = new();

    public int CurrentTurnTime => _currentTurnTime.Value;
    public ulong TurnPlayerId => _turnPlayerId.Value;

    public bool MyTurn => TurnPlayerId == NetworkManager.LocalClientId;

    public event TurnChange OnTurnChanged;
    public event TimeChange OnTimeChanged;

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

        _turnPlayerId.OnValueChanged += HandleTurnValueChanged;
        _currentTurnTime.OnValueChanged += HandleTimeValueChanged;

        if (IsServer)
        {

            _connectClientIds = NetworkManager.ConnectedClientsIds.ToList();
            _applyTurnTime = _turnTime;
            StartTurnLogic();

        }

    }

    private void StartTurnLogic()
    {

        _turnPlayerId.Value = GetRandomPlayerId();
        StartCoroutine(TurnPassCo());

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

    public void SkipTurn()
    {

        if (MyTurn)
        {

            SkipTurnServerRPC();

        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void SkipTurnServerRPC()
    {

        _isSkipped = true;

    }

    private IEnumerator TurnPassCo()
    {

        while (true)
        {

            bool isEnded = false;

            var co = StartCoroutine(TimePassCo(() => isEnded = true));

            yield return new WaitUntil(() => isEnded || _isSkipped);

            StopCoroutine(co);
            _isSkipped = false;

            if (isEnded)
            {

                var data = PlayerDataManager.Instance.Data;
                PlayerDataManager.Instance.AddHealth(-(int)data.state);

            }

            ChangeTurn();

        }

    }

    private IEnumerator TimePassCo(Action endCallback = null)
    {

        var wait = new WaitForSecondsRealtime(1);

        var apply = _currentTurnTime.Value = _applyTurnTime;

        for (int i = 0; i < apply; i++)
        {

            yield return wait;
            _currentTurnTime.Value -= 1;

        }

        endCallback?.Invoke();

    }

    public override void Dispose()
    {

        base.Dispose();

    }

    /// <summary>
    /// �� �ٲ�
    /// </summary>
    /// <param name="oldId">���� Id</param>
    /// <param name="newId">���ο� Id</param>
    public delegate void TurnChange(ulong oldId, ulong newId);

    /// <summary>
    /// �ð� �ٲ�
    /// </summary>
    /// <param name="oldTime">���� �ð�</param>
    /// <param name="newTime">���ο� �ð�</param>
    public delegate void TimeChange(int oldTime, int newTime);

}