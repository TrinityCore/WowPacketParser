using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionListOwnerItemsResult
    {
        public uint DesiredDelay;
        public List<CliAuctionItem> Items;
        public uint TotalCount;
    }
}
