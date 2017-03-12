using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBlackMarketRequestItemsResult
    {
        public UnixTime LastUpdateID;
        public List<ClientBlackMarketItem> Items;
    }
}
