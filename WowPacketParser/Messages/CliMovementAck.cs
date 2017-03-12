using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliMovementAck
    {
        public CliMovementStatus Status;
        public uint AckIndex;
    }
}
