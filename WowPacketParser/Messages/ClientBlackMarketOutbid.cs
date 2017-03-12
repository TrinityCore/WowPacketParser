using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBlackMarketOutbid
    {
        public ItemInstance Item;
        public int MarketID;
        public int RandomPropertiesID;
    }
}
