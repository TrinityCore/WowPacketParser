using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGBlackListSlot
    {
        public int Slot;
        public int Reason;
        public int SubReason1;
        public int SubReason2;
    }
}
