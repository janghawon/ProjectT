using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* Class: GameManager
* Author: 장하원
* Created: 2024년 6월 20일 목요일
* Description: 편한 기능들을 제공 받을 수 있는 GameManager
*/

[System.Serializable]
public struct PoolInfoGroup
{
    public Transform poolParent;
    public List<PoolListSO> poolListGroup;
}

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolInfoGroup _poolInfoGroup;

    private void Awake()
    {
        MakePool();

    }

    private void MakePool()
    {
        PoolManager poolManager = new PoolManager(_poolInfoGroup.poolParent);

        foreach (var poolList in _poolInfoGroup.poolListGroup)
        {
            foreach (var pool in poolList.poolList)
            {
                poolManager.CreatePool(pool.prefab, pool.type, pool.count);
            }
        }
    }

    public void LoadScene(string name)
    {

        NetworkManager.Singleton.SceneManager.LoadScene(name, LoadSceneMode.Additive);

    }

    public void UnLoadScene(string name)
    {

        NetworkManager.Singleton.SceneManager.UnloadScene(SceneManager.GetSceneByName(name));

    }


}
