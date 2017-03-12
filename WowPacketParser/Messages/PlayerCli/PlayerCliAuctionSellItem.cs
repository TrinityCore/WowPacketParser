using System.Collections.Generic;
using WowPacketParser.Messages.Client;

namespace WowPacketParser.Messages.PlayerCli
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
