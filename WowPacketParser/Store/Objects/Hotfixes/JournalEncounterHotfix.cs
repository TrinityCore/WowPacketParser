using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("journal_encounter")]
    public sealed record JournalEncounterHotfix1000: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("MapX")]
        public float? MapX;

        [DBFieldName("MapY")]
        public float? MapY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("JournalInstanceID")]
        public ushort? JournalInstanceID;

        [DBFieldName("DungeonEncounterID")]
        public ushort? DungeonEncounterID;

        [DBFieldName("OrderIndex")]
        public uint? OrderIndex;

        [DBFieldName("FirstSectionID")]
        public ushort? FirstSectionID;

        [DBFieldName("UiMapID")]
        public ushort? UiMapID;

        [DBFieldName("MapDisplayConditionID")]
        public uint? MapDisplayConditionID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("DifficultyMask")]
        public sbyte? DifficultyMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("journal_encounter_locale")]
    public sealed record JournalEncounterLocaleHotfix1000: IDataModel
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

    [Hotfix]
    [DBTableName("journal_encounter")]
    public sealed record JournalEncounterHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("MapX")]
        public float? MapX;

        [DBFieldName("MapY")]
        public float? MapY;

        [DBFieldName("JournalInstanceID")]
        public ushort? JournalInstanceID;

        [DBFieldName("OrderIndex")]
        public uint? OrderIndex;

        [DBFieldName("FirstSectionID")]
        public ushort? FirstSectionID;

        [DBFieldName("UiMapID")]
        public ushort? UiMapID;

        [DBFieldName("MapDisplayConditionID")]
        public uint? MapDisplayConditionID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("DifficultyMask")]
        public sbyte? DifficultyMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("journal_encounter_locale")]
    public sealed record JournalEncounterLocaleHotfix340: IDataModel
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
