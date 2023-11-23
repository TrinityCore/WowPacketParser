using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("journal_encounter_section")]
    public sealed record JournalEncounterSectionHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("BodyText")]
        public string BodyText;

        [DBFieldName("JournalEncounterID")]
        public ushort? JournalEncounterID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("ParentSectionID")]
        public ushort? ParentSectionID;

        [DBFieldName("FirstChildSectionID")]
        public ushort? FirstChildSectionID;

        [DBFieldName("NextSiblingSectionID")]
        public ushort? NextSiblingSectionID;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("IconCreatureDisplayInfoID")]
        public uint? IconCreatureDisplayInfoID;

        [DBFieldName("UiModelSceneID")]
        public int? UiModelSceneID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("IconFlags")]
        public int? IconFlags;

        [DBFieldName("DifficultyMask")]
        public sbyte? DifficultyMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("journal_encounter_section_locale")]
    public sealed record JournalEncounterSectionLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Title_lang")]
        public string TitleLang;

        [DBFieldName("BodyText_lang")]
        public string BodyTextLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("journal_encounter_section")]
    public sealed record JournalEncounterSectionHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("BodyText")]
        public string BodyText;

        [DBFieldName("JournalEncounterID")]
        public ushort? JournalEncounterID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("ParentSectionID")]
        public ushort? ParentSectionID;

        [DBFieldName("FirstChildSectionID")]
        public ushort? FirstChildSectionID;

        [DBFieldName("NextSiblingSectionID")]
        public ushort? NextSiblingSectionID;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("IconCreatureDisplayInfoID")]
        public uint? IconCreatureDisplayInfoID;

        [DBFieldName("UiModelSceneID")]
        public int? UiModelSceneID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("IconFlags")]
        public int? IconFlags;

        [DBFieldName("DifficultyMask")]
        public sbyte? DifficultyMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("journal_encounter_section_locale")]
    public sealed record JournalEncounterSectionLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Title_lang")]
        public string TitleLang;

        [DBFieldName("BodyText_lang")]
        public string BodyTextLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
