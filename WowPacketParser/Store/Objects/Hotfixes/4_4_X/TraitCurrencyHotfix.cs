using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_currency")]
    public sealed record TraitCurrencyHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Type")]
        public int? Type;

        [DBFieldName("CurrencyTypesID")]
        public int? CurrencyTypesID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Icon")]
        public int? Icon;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
