using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_reagents_currency")]
    public sealed record SpellReagentsCurrencyHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("CurrencyTypesID")]
        public ushort? CurrencyTypesID;

        [DBFieldName("CurrencyCount")]
        public ushort? CurrencyCount;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_reagents_currency")]
    public sealed record SpellReagentsCurrencyHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("CurrencyTypesID")]
        public ushort? CurrencyTypesID;

        [DBFieldName("CurrencyCount")]
        public ushort? CurrencyCount;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
