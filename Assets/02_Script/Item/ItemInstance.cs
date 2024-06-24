using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public abstract class ItemInstance : NetworkBehaviour
{

    [field: SerializeField] public ItemInfo Info { get; protected set; }

    private ulong _targetId;

    public override void OnNetworkSpawn()
    {

        _targetId = NetworkManager.ConnectedClientsIds.FirstOrDefault(x => x != OwnerClientId);

    }

    [ServerRpc(RequireOwnership = false)]
    private void UseItemServerRPC(ulong targetClientId)
    {

        UseItemClientRPC(targetClientId.GetRpcParams());

    }

    [ClientRpc]
    private void UseItemClientRPC(ClientRpcParams @params)
    {

        UseItem();

    }

    protected virtual void OnMouseOver()
    {
        //나중에 아웃라인
    }

    protected abstract void UseItem();

    protected virtual void OnMouseDown()
    { 

        if (IsOwner)
        {

            UseItem();
            UseItemServerRPC(_targetId);

        }

    }

}
