using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAuctionRemoveItem
    {
        public ulong Auctioneer;
        public int AuctionItemID;
    }
}
