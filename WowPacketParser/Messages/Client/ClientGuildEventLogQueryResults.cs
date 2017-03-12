using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildEventLogQueryResults
    {
        public List<ClientGuildEventEntry> Entry;
    }
}
