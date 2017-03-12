using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LFGListBlacklist
    {
        public List<LFGListBlacklistEntry> Entries;
    }
}
