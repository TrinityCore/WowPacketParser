using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_cost")]
    public sealed record TraitCostHotfix1000: IDataModel
    {
        [DBFieldName("InternalName")]
        public string InternalName;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Amount")]
        public int? Amount;

        [DBFieldName("TraitCurrencyID")]
        public int? TraitCurrencyID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_cost")]
    public sealed record TraitCostHotfix341: IDataModel
    {
        [DBFieldName("InternalName")]
        public string InternalName;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Amount")]
        public int? Amount;

        [DBFieldName("TraitCurrencyID")]
        public int? TraitCurrencyID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
