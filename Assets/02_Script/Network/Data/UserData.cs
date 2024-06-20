using Unity.Netcode;

[System.Serializable]
public struct UserData : INetworkSerializable
{

    public string nickName;
    public string authId;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref nickName);
        serializer.SerializeValue(ref authId);
    }
}