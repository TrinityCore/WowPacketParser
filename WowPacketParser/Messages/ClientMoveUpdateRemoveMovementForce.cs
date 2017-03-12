using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveUpdateRemoveMovementForce
    {
        public uint MovementForceID;
        public CliMovementStatus Status;
    }
}
