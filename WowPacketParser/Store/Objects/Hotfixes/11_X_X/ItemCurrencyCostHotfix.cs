using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_currency_cost")]
    public sealed record ItemCurrencyCostHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID")]
        public uint? ItemID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
