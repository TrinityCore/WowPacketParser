using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildEventLogQueryResults
    {
        public List<ClientGuildEventEntry> Entry;
    }
}
