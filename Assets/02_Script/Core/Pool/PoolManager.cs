using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolingType
{
    TVProduction
}

public class PoolManager
{
    public static PoolManager Instance;
    private Dictionary<PoolingType, Pool<PoolableMono>> _poolDic = new Dictionary<PoolingType, Pool<PoolableMono>>();
    private Transform _parentTrm;

    public PoolManager(Transform parentTrm)
    {
        if(Instance != null)
        {
            Debug.LogError("Error : PoolManager did not satisfy uniqueness");
            return;
        }

        Instance = this;

        _parentTrm = parentTrm;
    }
    
    public void CreatePool(PoolableMono prefab, PoolingType poolingType, int count = 10)
    {
        _poolDic.Add(poolingType, new Pool<PoolableMono>(prefab, poolingType, _parentTrm, count));
    }

    public void Push(PoolableMono obj)
    {
        if (_poolDic.ContainsKey(obj.poolingType))
        {
            _poolDic[obj.poolingType].Push(obj);
        }
        else
        {
            Debug.LogError($"not have ${obj.name} pool");
        }
    }

    public PoolableMono Pop(PoolingType type)
    {
        if (!_poolDic.ContainsKey(type))
        {
            Debug.LogError($"not have [${type}] pool");
            return null;
        }

        PoolableMono obj = _poolDic[type].Pop();
        obj.Init();

        return obj;
    }

    public bool IsContainPoolMono(PoolableMono obj)
    {
        return _poolDic[obj.poolingType].Contain(obj);
    }
}
