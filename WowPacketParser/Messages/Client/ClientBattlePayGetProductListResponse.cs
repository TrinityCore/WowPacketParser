using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePayGetProductListResponse
    {
        public List<BattlePayProduct> Products;
        public uint Result;
        public List<BattlePayProductGroup> Groups;
        public List<BattlePayShopEntry> ShopEntries;
        public uint CurrencyID;
    }
}
