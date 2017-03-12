using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveRemoveMovementForce
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public uint MovementForceID;
    }
}
