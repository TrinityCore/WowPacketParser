using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_currency_cost")]
    public sealed class ItemCurrencyCost
    {
        [DBFieldName("ItemId")]
        public uint ItemId;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
