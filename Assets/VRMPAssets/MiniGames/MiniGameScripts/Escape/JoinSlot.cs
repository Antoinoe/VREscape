using System;
using Unity.Netcode;

[Serializable]
public struct JoinSlot : INetworkSerializable, IEquatable<JoinSlot>
{
    public bool IsOccupied;
    public bool Equals(JoinSlot other)
    {
        return IsOccupied == other.IsOccupied;
    }
    public override bool Equals(object obj)
    {
        return obj is JoinSlot other && Equals(other);
    }

    public override int GetHashCode()
    {
        return IsOccupied.GetHashCode();
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer)
        where T : IReaderWriter
    {
        serializer.SerializeValue(ref IsOccupied);
    }
}
