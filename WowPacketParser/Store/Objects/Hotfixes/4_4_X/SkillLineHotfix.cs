using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("skill_line")]
    public sealed record SkillLineHotfix440: IDataModel
    {
        [DBFieldName("DisplayName")]
        public string DisplayName;

        [DBFieldName("AlternateVerb")]
        public string AlternateVerb;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("HordeDisplayName")]
        public string HordeDisplayName;

        [DBFieldName("OverrideSourceInfoDisplayName")]
        public string OverrideSourceInfoDisplayName;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CategoryID")]
        public sbyte? CategoryID;

        [DBFieldName("SpellIconFileID")]
        public int? SpellIconFileID;

        [DBFieldName("CanLink")]
        public sbyte? CanLink;

        [DBFieldName("ParentSkillLineID")]
        public uint? ParentSkillLineID;

        [DBFieldName("ParentTierIndex")]
        public int? ParentTierIndex;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("SpellBookSpellID")]
        public int? SpellBookSpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("skill_line_locale")]
    public sealed record SkillLineLocaleHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("DisplayName_lang")]
        public string DisplayNameLang;

        [DBFieldName("AlternateVerb_lang")]
        public string AlternateVerbLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("HordeDisplayName_lang")]
        public string HordeDisplayNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("skill_line")]
    public sealed record SkillLineHotfix441: IDataModel
    {
        [DBFieldName("DisplayName")]
        public string DisplayName;

        [DBFieldName("AlternateVerb")]
        public string AlternateVerb;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("HordeDisplayName")]
        public string HordeDisplayName;

        [DBFieldName("OverrideSourceInfoDisplayName")]
        public string OverrideSourceInfoDisplayName;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CategoryID")]
        public sbyte? CategoryID;

        [DBFieldName("SpellIconFileID")]
        public int? SpellIconFileID;

        [DBFieldName("CanLink")]
        public sbyte? CanLink;

        [DBFieldName("ParentSkillLineID")]
        public uint? ParentSkillLineID;

        [DBFieldName("ParentTierIndex")]
        public int? ParentTierIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SpellBookSpellID")]
        public int? SpellBookSpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("skill_line_locale")]
    public sealed record SkillLineLocaleHotfix441: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("DisplayName_lang")]
        public string DisplayNameLang;

        [DBFieldName("AlternateVerb_lang")]
        public string AlternateVerbLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("HordeDisplayName_lang")]
        public string HordeDisplayNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
