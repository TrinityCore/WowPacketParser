using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
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
