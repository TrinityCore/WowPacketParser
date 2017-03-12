using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LFGListBlacklistEntry
    {
        public uint ActivityID;
        public uint Reason;
    }
}
