using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionListPendingSalesResult
    {
        public List<CliMailListEntry> Mails;
        public int TotalNumRecords;
    }
}
