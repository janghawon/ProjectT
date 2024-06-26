using Unity.Netcode;
using UnityEngine;

public static class Support
{
    
    public static ClientRpcParams GetRpcParams(this ulong id)
    {

        return new ClientRpcParams { Send = new() { TargetClientIds = new[] { id } } };

    }

    public static void Clear(this Transform target)
    {

        int cnt = target.childCount;

        for(int i = 0; i < cnt; i++)
        {

            Object.Destroy(target.GetChild(i).gameObject);

        }

    }

}
