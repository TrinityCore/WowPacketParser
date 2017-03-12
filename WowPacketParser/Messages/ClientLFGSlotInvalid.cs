using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGSlotInvalid
    {
        public int SubReason1;
        public int SubReason2;
        public uint Reason;
    }
}
