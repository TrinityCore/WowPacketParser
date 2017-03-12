using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct WhoResponse
    {
        public List<WhoEntry> Entries;
    }
}
