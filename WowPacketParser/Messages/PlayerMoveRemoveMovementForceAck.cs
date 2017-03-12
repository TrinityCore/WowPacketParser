using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveRemoveMovementForceAck
    {
        public CliMovementAck Data;
        public uint MovementForceID;
    }
}
