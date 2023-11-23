using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("talent")]
    public sealed record TalentHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("TierID")]
        public byte? TierID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ColumnIndex")]
        public byte? ColumnIndex;

        [DBFieldName("ClassID")]
        public byte? ClassID;

        [DBFieldName("SpecID")]
        public ushort? SpecID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("OverridesSpellID")]
        public uint? OverridesSpellID;

        [DBFieldName("CategoryMask", 2)]
        public byte?[] CategoryMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("talent_locale")]
    public sealed record TalentLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("talent")]
    public sealed record TalentHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("TierID")]
        public byte? TierID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ColumnIndex")]
        public byte? ColumnIndex;

        [DBFieldName("TabID")]
        public ushort? TabID;

        [DBFieldName("ClassID")]
        public byte? ClassID;

        [DBFieldName("SpecID")]
        public ushort? SpecID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("OverridesSpellID")]
        public int? OverridesSpellID;

        [DBFieldName("RequiredSpellID")]
        public int? RequiredSpellID;

        [DBFieldName("CategoryMask", 2)]
        public int?[] CategoryMask;

        [DBFieldName("SpellRank", 9)]
        public int?[] SpellRank;

        [DBFieldName("PrereqTalent", 3)]
        public int?[] PrereqTalent;

        [DBFieldName("PrereqRank", 3)]
        public int?[] PrereqRank;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("talent_locale")]
    public sealed record TalentLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
