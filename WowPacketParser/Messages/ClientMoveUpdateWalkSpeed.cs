using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveUpdateWalkSpeed
    {
        public CliMovementStatus Status;
        public float Speed;
    }
}
