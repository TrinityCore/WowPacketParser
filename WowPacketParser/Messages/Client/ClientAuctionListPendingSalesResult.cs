using System.Collections.Generic;
using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuctionListPendingSalesResult
    {
        public List<CliMailListEntry> Mails;
        public int TotalNumRecords;
    }
}
