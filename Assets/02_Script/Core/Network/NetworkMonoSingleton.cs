using System;
using Unity.Netcode;
using UnityEngine;

public abstract class NetworkMonoSingleton<T> : NetworkBehaviour, IDisposable
    where T : NetworkBehaviour
{

    private static T _instance = null;
    private static bool _isQuitting = false;

    public static T Instance
    {
        get
        {
            // 비활성화 됐다면 기존꺼 내비두고 새로 만든다.
            if (_isQuitting)
            {
                _instance = null;
            }

            // instance가 NULL일때 새로 생성한다.
            if (_instance == null)
            {
                _instance = GameObject.FindAnyObjectByType<T>();

                if (_instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} is not exits");
                }
                else
                {
                    _isQuitting = false; //재사용 용도면.
                }

            }
            return _instance;

        }
    }

    public virtual void Dispose()
    {
        _isQuitting = true;
        _instance = null;
    }

    public override void OnNetworkDespawn()
    {

        base.OnNetworkDespawn();
        Dispose();

    }

    private void OnDisable()
    {

        Dispose();

    }

}
