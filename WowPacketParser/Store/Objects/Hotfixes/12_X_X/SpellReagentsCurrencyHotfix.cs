using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_reagents_currency")]
    public sealed record SpellReagentsCurrencyHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("CurrencyTypesID")]
        public int? CurrencyTypesID;

        [DBFieldName("CurrencyCount")]
        public int? CurrencyCount;

        [DBFieldName("OverrideRecraftCurrencyCount")]
        public int? OverrideRecraftCurrencyCount;

        [DBFieldName("OrderSource")]
        public byte? OrderSource;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
