using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserRouterClientSuspendCommsAck
    {
        public uint Serial;
        public uint Timestamp;
    }
}
