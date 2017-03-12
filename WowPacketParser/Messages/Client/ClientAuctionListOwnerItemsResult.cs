using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuctionListOwnerItemsResult
    {
        public uint DesiredDelay;
        public List<CliAuctionItem> Items;
        public uint TotalCount;
    }
}
