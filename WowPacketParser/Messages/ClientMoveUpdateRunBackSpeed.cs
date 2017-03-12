using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveUpdateRunBackSpeed
    {
        public float Speed;
        public CliMovementStatus Status;
    }
}
