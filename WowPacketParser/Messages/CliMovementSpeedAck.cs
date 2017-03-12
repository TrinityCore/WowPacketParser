using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliMovementSpeedAck
    {
        public CliMovementAck Ack;
        public float Speed;
    }
}
