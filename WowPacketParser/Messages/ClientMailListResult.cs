using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMailListResult
    {
        public int TotalNumRecords;
        public List<CliMailListEntry> Mails;
    }
}
