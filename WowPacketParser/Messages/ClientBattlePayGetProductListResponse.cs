using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
