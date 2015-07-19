using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_power")]
    public sealed class SpellReagents
    {
        [DBFieldName("Reagent", 8)]
        public int[] Reagent;

        [DBFieldName("ReagentCount", 8)]
        public uint[] ReagentCount;

        [DBFieldName("CurrencyID")]
        public uint CurrencyID;

        [DBFieldName("CurrencyCount")]
        public uint CurrencyCount;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
