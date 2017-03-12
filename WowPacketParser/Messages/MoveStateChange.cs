using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct MoveStateChange
    {
        public ushort MessageID;
        public uint SequenceIndex;
        public float Speed; // Optional
        public KnockBackInfo KnockBack; // Optional
        public int VehicleRecID; // Optional
        public CollisionHeightInfo CollisionHeight; // Optional
        public CliMovementForce MovementForce; // Optional
    }
}
