using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_categories")]
    public sealed record SpellCategoriesHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("Category")]
        public short? Category;

        [DBFieldName("DefenseType")]
        public sbyte? DefenseType;

        [DBFieldName("DispelType")]
        public sbyte? DispelType;

        [DBFieldName("Mechanic")]
        public sbyte? Mechanic;

        [DBFieldName("PreventionType")]
        public sbyte? PreventionType;

        [DBFieldName("StartRecoveryCategory")]
        public short? StartRecoveryCategory;

        [DBFieldName("ChargeCategory")]
        public short? ChargeCategory;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_categories")]
    public sealed record SpellCategoriesHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("Category")]
        public short? Category;

        [DBFieldName("DefenseType")]
        public sbyte? DefenseType;

        [DBFieldName("DispelType")]
        public sbyte? DispelType;

        [DBFieldName("Mechanic")]
        public sbyte? Mechanic;

        [DBFieldName("PreventionType")]
        public sbyte? PreventionType;

        [DBFieldName("StartRecoveryCategory")]
        public short? StartRecoveryCategory;

        [DBFieldName("ChargeCategory")]
        public short? ChargeCategory;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
