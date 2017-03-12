using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliBlackMarketBidOnItem
    {
        public ulong NpcGUID;
        public ItemInstance Item;
        public int MarketID;
        public ulong BidAmount;
    }
}
