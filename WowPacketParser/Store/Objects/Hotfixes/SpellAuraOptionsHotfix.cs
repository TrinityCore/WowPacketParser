using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_aura_options")]
    public sealed record SpellAuraOptionsHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("CumulativeAura")]
        public ushort? CumulativeAura;

        [DBFieldName("ProcCategoryRecovery")]
        public int? ProcCategoryRecovery;

        [DBFieldName("ProcChance")]
        public byte? ProcChance;

        [DBFieldName("ProcCharges")]
        public int? ProcCharges;

        [DBFieldName("SpellProcsPerMinuteID")]
        public ushort? SpellProcsPerMinuteID;

        [DBFieldName("ProcTypeMask", 2)]
        public int?[] ProcTypeMask;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
