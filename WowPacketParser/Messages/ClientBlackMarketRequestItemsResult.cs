using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBlackMarketRequestItemsResult
    {
        public UnixTime LastUpdateID;
        public List<ClientBlackMarketItem> Items;
    }
}
