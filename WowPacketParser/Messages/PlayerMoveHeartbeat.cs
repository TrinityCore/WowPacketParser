using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveHeartbeat
    {
        public CliMovementStatus Status;
    }
}
