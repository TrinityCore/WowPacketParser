using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveApplyMovementForceAck
    {
        public int TransportID;
        public Vector2 Direction;
        public uint MovementForceID;
        public CliMovementAck Data;
    }
}
