using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveForceTurnRateChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
