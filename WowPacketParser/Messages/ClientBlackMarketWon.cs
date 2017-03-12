using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBlackMarketWon
    {
        public int MarketID;
        public int RandomPropertiesID;
        public ItemInstance Item;
    }
}
