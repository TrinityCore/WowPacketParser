using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("talent_tab")]
    public sealed record TalentTabHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("BackgroundFile")]
        public string BackgroundFile;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("RaceMask")]
        public int? RaceMask;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("PetTalentMask")]
        public int? PetTalentMask;

        [DBFieldName("SpellIconID")]
        public int? SpellIconID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("talent_tab_locale")]
    public sealed record TalentTabLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("talent_tab")]
    public sealed record TalentTabHotfix344: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("BackgroundFile")]
        public string BackgroundFile;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("Field44053627004")]
        public int? Field44053627004;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("CategoryEnumID")]
        public int? CategoryEnumID;

        [DBFieldName("SpellIconID")]
        public int? SpellIconID;

        [DBFieldName("RoleMask")]
        public int? RoleMask;

        [DBFieldName("MasterySpellID", 2)]
        public int?[] MasterySpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("talent_tab_locale")]
    public sealed record TalentTabLocaleHotfix344: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
