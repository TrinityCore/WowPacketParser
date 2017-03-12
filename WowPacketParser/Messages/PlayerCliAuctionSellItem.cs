using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAuctionSellItem
    {
        public ulong BuyoutPrice;
        public List<ClientAuctionItemForSale> Items;
        public ulong Auctioneer;
        public ulong MinBid;
        public uint RunTime;
    }
}
