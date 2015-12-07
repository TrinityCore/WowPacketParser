using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_reagents")]
    public sealed class SpellReagents : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Reagent", 8)]
        public int?[] Reagent;

        [DBFieldName("ReagentCount", 8)]
        public uint?[] ReagentCount;

        [DBFieldName("CurrencyID")]
        public uint? CurrencyID;

        [DBFieldName("CurrencyCount")]
        public uint? CurrencyCount;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
