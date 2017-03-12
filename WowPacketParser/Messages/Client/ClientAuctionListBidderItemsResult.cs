using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuctionListBidderItemsResult
    {
        public uint DesiredDelay;
        public List<CliAuctionItem> Items;
        public uint TotalCount;
    }
}
