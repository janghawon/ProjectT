using Unity.Netcode;
using UnityEngine;

public static class Support
{
    
    public static ClientRpcParams GetRpcParams(this ulong id)
    {

        return new ClientRpcParams { Send = new() { TargetClientIds = new[] { id } } };

    }

}
