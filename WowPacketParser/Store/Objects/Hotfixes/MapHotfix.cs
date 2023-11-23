using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("map")]
    public sealed record MapHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Directory")]
        public string Directory;

        [DBFieldName("MapName")]
        public string MapName;

        [DBFieldName("MapDescription0")]
        public string MapDescription0;

        [DBFieldName("MapDescription1")]
        public string MapDescription1;

        [DBFieldName("PvpShortDescription")]
        public string PvpShortDescription;

        [DBFieldName("PvpLongDescription")]
        public string PvpLongDescription;

        [DBFieldName("CorpseX")]
        public float? CorpseX;

        [DBFieldName("CorpseY")]
        public float? CorpseY;

        [DBFieldName("MapType")]
        public byte? MapType;

        [DBFieldName("InstanceType")]
        public sbyte? InstanceType;

        [DBFieldName("ExpansionID")]
        public byte? ExpansionID;

        [DBFieldName("AreaTableID")]
        public ushort? AreaTableID;

        [DBFieldName("LoadingScreenID")]
        public short? LoadingScreenID;

        [DBFieldName("TimeOfDayOverride")]
        public short? TimeOfDayOverride;

        [DBFieldName("ParentMapID")]
        public short? ParentMapID;

        [DBFieldName("CosmeticParentMapID")]
        public short? CosmeticParentMapID;

        [DBFieldName("TimeOffset")]
        public byte? TimeOffset;

        [DBFieldName("MinimapIconScale")]
        public float? MinimapIconScale;

        [DBFieldName("CorpseMapID")]
        public short? CorpseMapID;

        [DBFieldName("MaxPlayers")]
        public byte? MaxPlayers;

        [DBFieldName("WindSettingsID")]
        public short? WindSettingsID;

        [DBFieldName("ZmpFileDataID")]
        public int? ZmpFileDataID;

        [DBFieldName("WdtFileDataID")]
        public int? WdtFileDataID;

        [DBFieldName("NavigationMaxDistance")]
        public int? NavigationMaxDistance;

        [DBFieldName("Flags", 3)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_locale")]
    public sealed record MapLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("MapName_lang")]
        public string MapNameLang;

        [DBFieldName("MapDescription0_lang")]
        public string MapDescription0Lang;

        [DBFieldName("MapDescription1_lang")]
        public string MapDescription1Lang;

        [DBFieldName("PvpShortDescription_lang")]
        public string PvpShortDescriptionLang;

        [DBFieldName("PvpLongDescription_lang")]
        public string PvpLongDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map")]
    public sealed record MapHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Directory")]
        public string Directory;

        [DBFieldName("MapName")]
        public string MapName;

        [DBFieldName("MapDescription0")]
        public string MapDescription0;

        [DBFieldName("MapDescription1")]
        public string MapDescription1;

        [DBFieldName("PvpShortDescription")]
        public string PvpShortDescription;

        [DBFieldName("PvpLongDescription")]
        public string PvpLongDescription;

        [DBFieldName("MapType")]
        public byte? MapType;

        [DBFieldName("InstanceType")]
        public sbyte? InstanceType;

        [DBFieldName("ExpansionID")]
        public byte? ExpansionID;

        [DBFieldName("AreaTableID")]
        public ushort? AreaTableID;

        [DBFieldName("LoadingScreenID")]
        public short? LoadingScreenID;

        [DBFieldName("TimeOfDayOverride")]
        public short? TimeOfDayOverride;

        [DBFieldName("ParentMapID")]
        public short? ParentMapID;

        [DBFieldName("CosmeticParentMapID")]
        public short? CosmeticParentMapID;

        [DBFieldName("TimeOffset")]
        public byte? TimeOffset;

        [DBFieldName("MinimapIconScale")]
        public float? MinimapIconScale;

        [DBFieldName("RaidOffset")]
        public int? RaidOffset;

        [DBFieldName("CorpseMapID")]
        public short? CorpseMapID;

        [DBFieldName("MaxPlayers")]
        public byte? MaxPlayers;

        [DBFieldName("WindSettingsID")]
        public short? WindSettingsID;

        [DBFieldName("ZmpFileDataID")]
        public int? ZmpFileDataID;

        [DBFieldName("Flags", 3)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_locale")]
    public sealed record MapLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("MapName_lang")]
        public string MapNameLang;

        [DBFieldName("MapDescription0_lang")]
        public string MapDescription0Lang;

        [DBFieldName("MapDescription1_lang")]
        public string MapDescription1Lang;

        [DBFieldName("PvpShortDescription_lang")]
        public string PvpShortDescriptionLang;

        [DBFieldName("PvpLongDescription_lang")]
        public string PvpLongDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
