using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("talent")]
    public sealed record TalentHotfix1100: IDataModel
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
    public sealed record TalentLocaleHotfix1100: IDataModel
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
    public sealed record TalentHotfix1110 : IDataModel
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
        public sbyte? ClassID;

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
    public sealed record TalentLocaleHotfix1110 : IDataModel
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
    public sealed record TalentHotfix1115 : IDataModel
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
        public sbyte? ClassID;

        [DBFieldName("SpecID")]
        public ushort? SpecID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("OverridesSpellID")]
        public uint? OverridesSpellID;

        [DBFieldName("RequiredSpellID")]
        public uint? RequiredSpellID;

        [DBFieldName("CategoryMask", 2)]
        public int?[] CategoryMask;

        [DBFieldName("SpellRank", 9)]
        public uint?[] SpellRank;

        [DBFieldName("PrereqTalent", 3)]
        public uint?[] PrereqTalent;

        [DBFieldName("PrereqRank", 3)]
        public byte?[] PrereqRank;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("talent_locale")]
    public sealed record TalentLocaleHotfix1115 : IDataModel
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
    public sealed record TalentHotfix1117 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("TierID")]
        public byte? TierID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ColumnIndex")]
        public byte? ColumnIndex;

        [DBFieldName("TabID")]
        public ushort? TabID;

        [DBFieldName("ClassID")]
        public sbyte? ClassID;

        [DBFieldName("SpecID")]
        public ushort? SpecID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("OverridesSpellID")]
        public uint? OverridesSpellID;

        [DBFieldName("RequiredSpellID")]
        public uint? RequiredSpellID;

        [DBFieldName("CategoryMask", 2)]
        public int?[] CategoryMask;

        [DBFieldName("SpellRank", 9)]
        public uint?[] SpellRank;

        [DBFieldName("PrereqTalent", 3)]
        public uint?[] PrereqTalent;

        [DBFieldName("PrereqRank", 3)]
        public byte?[] PrereqRank;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("talent_locale")]
    public sealed record TalentLocaleHotfix1117 : IDataModel
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
