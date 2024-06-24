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
            // ��Ȱ��ȭ �ƴٸ� ������ ����ΰ� ���� �����.
            if (_isQuitting)
            {
                _instance = null;
            }

            // instance�� NULL�϶� ���� �����Ѵ�.
            if (_instance == null)
            {
                _instance = GameObject.FindAnyObjectByType<T>();

                if (_instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} is not exits");
                }
                else
                {
                    _isQuitting = false; //���� �뵵��.
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
