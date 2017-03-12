using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuctionListItemsResult
    {
        public uint DesiredDelay;
        public List<CliAuctionItem> Items;
        public bool OnlyUsable;
        public uint TotalCount;
    }
}
