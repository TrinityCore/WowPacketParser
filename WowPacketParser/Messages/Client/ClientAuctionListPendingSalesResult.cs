using System.Collections.Generic;
using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuctionListPendingSalesResult
    {
        public List<CliMailListEntry> Mails;
        public int TotalNumRecords;
    }
}
