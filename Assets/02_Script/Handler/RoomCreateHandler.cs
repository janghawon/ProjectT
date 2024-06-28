using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class RoomCreateHandler : MonoBehaviour
{

    [SerializeField] private UnityEvent<string> _onRoomCreated;

    private void Start()
    {

        if (NetworkManager.Singleton.IsHost)
        {

            if (!HostSingle.Instance.GameManager.JoinCode.Equals(string.Empty))
            {

                _onRoomCreated?.Invoke(HostSingle.Instance.GameManager.JoinCode);

            }
            else
            {

                HostSingle.Instance.GameManager.OnRoomCreated += HandleRoomCreated;

            }


        }
        else
        {

            HostSingle.Instance.GameManager.OnRoomCreated += HandleRoomCreated;

        }


    }

    private void HandleRoomCreated(string obj)
    {

        _onRoomCreated?.Invoke(obj);

    }

}
