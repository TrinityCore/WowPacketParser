using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LFGBlackListSlot
    {
        public uint Slot;
        public uint Reason;
        public int SubReason1;
        public int SubReason2;
    }
}
