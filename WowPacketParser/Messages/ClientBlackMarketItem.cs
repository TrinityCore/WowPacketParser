using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBlackMarketItem
    {
        public int MarketID;
        public int SellerNPC;
        public ItemInstance Item;
        public int Quantity;
        public ulong MinBid;
        public ulong MinIncrement;
        public ulong CurrentBid;
        public int SecondsRemaining;
        public bool HighBid;
        public int NumBids;
    }
}
