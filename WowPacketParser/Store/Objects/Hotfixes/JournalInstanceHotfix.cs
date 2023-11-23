using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("journal_instance")]
    public sealed record JournalInstanceHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("BackgroundFileDataID")]
        public int? BackgroundFileDataID;

        [DBFieldName("ButtonFileDataID")]
        public int? ButtonFileDataID;

        [DBFieldName("ButtonSmallFileDataID")]
        public int? ButtonSmallFileDataID;

        [DBFieldName("LoreFileDataID")]
        public int? LoreFileDataID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AreaID")]
        public ushort? AreaID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("journal_instance_locale")]
    public sealed record JournalInstanceLocaleHotfix1000: IDataModel
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
    [DBTableName("journal_instance")]
    public sealed record JournalInstanceHotfix1002: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("BackgroundFileDataID")]
        public int? BackgroundFileDataID;

        [DBFieldName("ButtonFileDataID")]
        public int? ButtonFileDataID;

        [DBFieldName("ButtonSmallFileDataID")]
        public int? ButtonSmallFileDataID;

        [DBFieldName("LoreFileDataID")]
        public int? LoreFileDataID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AreaID")]
        public ushort? AreaID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("journal_instance_locale")]
    public sealed record JournalInstanceLocaleHotfix1002: IDataModel
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
    [DBTableName("journal_instance")]
    public sealed record JournalInstanceHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("BackgroundFileDataID")]
        public int? BackgroundFileDataID;

        [DBFieldName("ButtonFileDataID")]
        public int? ButtonFileDataID;

        [DBFieldName("ButtonSmallFileDataID")]
        public int? ButtonSmallFileDataID;

        [DBFieldName("LoreFileDataID")]
        public int? LoreFileDataID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AreaID")]
        public ushort? AreaID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("journal_instance_locale")]
    public sealed record JournalInstanceLocaleHotfix340: IDataModel
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
    [DBTableName("journal_instance")]
    public sealed record JournalInstanceHotfix341: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("BackgroundFileDataID")]
        public int? BackgroundFileDataID;

        [DBFieldName("ButtonFileDataID")]
        public int? ButtonFileDataID;

        [DBFieldName("ButtonSmallFileDataID")]
        public int? ButtonSmallFileDataID;

        [DBFieldName("LoreFileDataID")]
        public int? LoreFileDataID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AreaID")]
        public ushort? AreaID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("journal_instance_locale")]
    public sealed record JournalInstanceLocaleHotfix341: IDataModel
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
