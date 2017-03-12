using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserRouterClientPing
    {
        public uint Serial;
        public uint Latency;
    }
}
