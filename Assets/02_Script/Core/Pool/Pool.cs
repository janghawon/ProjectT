using System;
using System.Collections.Generic;
using UnityEngine;

class Pool<T> where T : PoolableMono
{
    private Stack<T> _pool = new Stack<T>();
    private T _prefab; //오리지널 저장
    private Transform _parent;
    private PoolingType _poolingType;

    public Pool(T prefab, PoolingType poolingType, Transform parent, int count = 5)
    {
        _prefab = prefab;
        _parent = parent;
        _poolingType = poolingType;

        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.poolingType = _poolingType;
            obj.gameObject.name = _poolingType.ToString();
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }

    public T Pop()
    {
        T toGetPoolObj = null;

        if (_pool.Count <= 0)
        {
            toGetPoolObj = GameObject.Instantiate(_prefab, _parent);
            toGetPoolObj.gameObject.name = _poolingType.ToString();
            toGetPoolObj.poolingType = _poolingType;
        }
        else
        {
            toGetPoolObj = _pool.Pop();
            toGetPoolObj.gameObject.SetActive(true);
        }
        return toGetPoolObj;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(_parent);
        _pool.Push(obj);
    }

    public bool Contain(T obj)
    {
        return _pool.Contains(obj);
    }
}