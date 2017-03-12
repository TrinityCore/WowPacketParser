using System.Collections.Generic;
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
