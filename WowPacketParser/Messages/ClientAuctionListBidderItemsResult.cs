using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionListBidderItemsResult
    {
        public uint DesiredDelay;
        public List<CliAuctionItem> Items;
        public uint TotalCount;
    }
}
