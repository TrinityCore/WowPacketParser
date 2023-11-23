using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("reward_pack_x_currency_type")]
    public sealed record RewardPackXCurrencyTypeHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CurrencyTypeID")]
        public uint? CurrencyTypeID;

        [DBFieldName("Quantity")]
        public int? Quantity;

        [DBFieldName("RewardPackID")]
        public uint? RewardPackID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("reward_pack_x_currency_type")]
    public sealed record RewardPackXCurrencyTypeHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CurrencyTypeID")]
        public uint? CurrencyTypeID;

        [DBFieldName("Quantity")]
        public int? Quantity;

        [DBFieldName("RewardPackID")]
        public int? RewardPackID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
