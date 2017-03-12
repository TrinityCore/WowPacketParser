using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionListItemsResult
    {
        public uint DesiredDelay;
        public List<CliAuctionItem> Items;
        public bool OnlyUsable;
        public uint TotalCount;
    }
}
