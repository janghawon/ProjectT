using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{

    /// <summary>
    /// ������ ���̸� ǥ���ҰŴ�?
    /// </summary>
    [SerializeField] private bool _isLocal;
    [SerializeField] private Transform _healthRoot;
    [SerializeField] private GameObject _healthPrefab;

    private void Awake()
    {

        PlayerDataManager.Instance.OnPlayerDataChanged += HandleDataChanged;

    }

    private void HandleDataChanged(PlayerData changeData)
    {
        var data = _isLocal ?
            PlayerDataManager.Instance.Data :
            PlayerDataManager.Instance[GamePlayManager.Instance.EnemyClientId];

        _healthRoot.Clear();

        for (int i = 0; i < data.health; i++)
        {

            Instantiate(_healthPrefab, _healthRoot);

        }
    }

}
