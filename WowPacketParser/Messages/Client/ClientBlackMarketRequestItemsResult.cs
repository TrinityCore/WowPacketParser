using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBlackMarketRequestItemsResult
    {
        public UnixTime LastUpdateID;
        public List<ClientBlackMarketItem> Items;
    }
}
