using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBlackMarketBidOnItemResult
    {
        public int MarketID;
        public ItemInstance Item;
        public int Result;
    }
}
