using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveUpdateFlightBackSpeed
    {
        public float Speed;
        public CliMovementStatus Status;
    }
}
