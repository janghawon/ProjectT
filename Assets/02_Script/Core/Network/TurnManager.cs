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
    /// 스킵 함?
    /// </summary>
    private bool _isSkipped;

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
            ulong.MaxValue,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server);

    private List<ulong> _connectClientIds = new();
    private int _turnCount;

    public int CurrentTurnTime => _currentTurnTime.Value;
    public ulong TurnPlayerId => _turnPlayerId.Value;

    public bool MyTurn => TurnPlayerId == NetworkManager.LocalClientId;
    private NetworkVariable<bool> _isGoldTime = new NetworkVariable<bool>(
            false,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server);

    public bool IsGoldTime => _isGoldTime.Value;

    public event TurnChange OnTurnChanged;
    public event TimeChange OnTimeChanged;
    public event Action OnGoldTimeStart;

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

        if(_turnCount == 6 && !IsGoldTime)
        {

            _isGoldTime.Value = true;
            _turnCount = 0;
            Debug.Log("골드 타임");
            OnGoldTimeStart?.Invoke();

        }

        _turnCount++;
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

        yield return null;
        _turnPlayerId.Value = GetRandomPlayerId();

        while (true)
        {

            bool isEnded = false;

            var co = StartCoroutine(TimePassCo(() => isEnded = true));

            yield return new WaitUntil(() => isEnded || _isSkipped);

            StopCoroutine(co);
            _isSkipped = false;

            if (isEnded)
            {

                var data = PlayerDataManager.Instance[_turnPlayerId.Value];
                data.extraTurnTime = 30;
                PlayerDataManager.Instance.SetData(data);
                PlayerDataManager.Instance.AddHealth(_turnPlayerId.Value, -(int)data.state);

            }
            else
            {

                var data = PlayerDataManager.Instance[_turnPlayerId.Value];
                data.extraTurnTime = 0;
                PlayerDataManager.Instance.SetData(data);

            }

            ChangeTurn();

        }

    }

    private IEnumerator TimePassCo(Action endCallback = null)
    {

        var wait = new WaitForSecondsRealtime(1);

        int v = IsGoldTime ? 10 * _turnCount : PlayerDataManager.Instance[_turnPlayerId.Value].extraTurnTime;
        var apply = _currentTurnTime.Value = _applyTurnTime - v;

        if(apply <= 0)
        {

            GamePlayManager.Instance.CheckEndGame();
            yield break;

        }

        for (int i = 0; i < apply; i++)
        {

            yield return wait;
            _currentTurnTime.Value -= 1;

        }
        endCallback?.Invoke();

    }

    public void SetTurnCount(int count)
    {

        _turnCount = count;

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