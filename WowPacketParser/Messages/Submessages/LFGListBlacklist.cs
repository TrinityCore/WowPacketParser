using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LFGListBlacklist
    {
        public List<LFGListBlacklistEntry> Entries;
    }
}
