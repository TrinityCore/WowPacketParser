using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct WhoResponse
    {
        public List<WhoEntry> Entries;
    }
}
