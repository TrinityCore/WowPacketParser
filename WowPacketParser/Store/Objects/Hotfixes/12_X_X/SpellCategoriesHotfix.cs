using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_categories")]
    public sealed record SpellCategoriesHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public short? DifficultyID;

        [DBFieldName("Category")]
        public short? Category;

        [DBFieldName("DefenseType")]
        public sbyte? DefenseType;

        [DBFieldName("DiminishType")]
        public int? DiminishType;

        [DBFieldName("DispelType")]
        public sbyte? DispelType;

        [DBFieldName("Mechanic")]
        public sbyte? Mechanic;

        [DBFieldName("PreventionType")]
        public int? PreventionType;

        [DBFieldName("StartRecoveryCategory")]
        public short? StartRecoveryCategory;

        [DBFieldName("ChargeCategory")]
        public short? ChargeCategory;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
