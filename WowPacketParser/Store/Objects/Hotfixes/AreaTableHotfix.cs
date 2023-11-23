using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("area_table")]
    public sealed record AreaTableHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ZoneName")]
        public string ZoneName;

        [DBFieldName("AreaName")]
        public string AreaName;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("ParentAreaID")]
        public ushort? ParentAreaID;

        [DBFieldName("AreaBit")]
        public short? AreaBit;

        [DBFieldName("SoundProviderPref")]
        public byte? SoundProviderPref;

        [DBFieldName("SoundProviderPrefUnderwater")]
        public byte? SoundProviderPrefUnderwater;

        [DBFieldName("AmbienceID")]
        public ushort? AmbienceID;

        [DBFieldName("UwAmbience")]
        public ushort? UwAmbience;

        [DBFieldName("ZoneMusic")]
        public ushort? ZoneMusic;

        [DBFieldName("UwZoneMusic")]
        public ushort? UwZoneMusic;

        [DBFieldName("IntroSound")]
        public ushort? IntroSound;

        [DBFieldName("UwIntroSound")]
        public uint? UwIntroSound;

        [DBFieldName("FactionGroupMask")]
        public byte? FactionGroupMask;

        [DBFieldName("AmbientMultiplier")]
        public float? AmbientMultiplier;

        [DBFieldName("MountFlags")]
        public byte? MountFlags;

        [DBFieldName("PvpCombatWorldStateID")]
        public short? PvpCombatWorldStateID;

        [DBFieldName("WildBattlePetLevelMin")]
        public byte? WildBattlePetLevelMin;

        [DBFieldName("WildBattlePetLevelMax")]
        public byte? WildBattlePetLevelMax;

        [DBFieldName("WindSettingsID")]
        public byte? WindSettingsID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("LiquidTypeID", 4)]
        public ushort?[] LiquidTypeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_table_locale")]
    public sealed record AreaTableLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("AreaName_lang")]
        public string AreaNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_table")]
    public sealed record AreaTableHotfix1015 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ZoneName")]
        public string ZoneName;

        [DBFieldName("AreaName")]
        public string AreaName;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("ParentAreaID")]
        public ushort? ParentAreaID;

        [DBFieldName("AreaBit")]
        public short? AreaBit;

        [DBFieldName("SoundProviderPref")]
        public byte? SoundProviderPref;

        [DBFieldName("SoundProviderPrefUnderwater")]
        public byte? SoundProviderPrefUnderwater;

        [DBFieldName("AmbienceID")]
        public ushort? AmbienceID;

        [DBFieldName("UwAmbience")]
        public ushort? UwAmbience;

        [DBFieldName("ZoneMusic")]
        public ushort? ZoneMusic;

        [DBFieldName("UwZoneMusic")]
        public ushort? UwZoneMusic;

        [DBFieldName("IntroSound")]
        public ushort? IntroSound;

        [DBFieldName("UwIntroSound")]
        public uint? UwIntroSound;

        [DBFieldName("FactionGroupMask")]
        public byte? FactionGroupMask;

        [DBFieldName("AmbientMultiplier")]
        public float? AmbientMultiplier;

        [DBFieldName("MountFlags")]
        public int? MountFlags;

        [DBFieldName("PvpCombatWorldStateID")]
        public short? PvpCombatWorldStateID;

        [DBFieldName("WildBattlePetLevelMin")]
        public byte? WildBattlePetLevelMin;

        [DBFieldName("WildBattlePetLevelMax")]
        public byte? WildBattlePetLevelMax;

        [DBFieldName("WindSettingsID")]
        public byte? WindSettingsID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("LiquidTypeID", 4)]
        public ushort?[] LiquidTypeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_table_locale")]
    public sealed record AreaTableLocaleHotfix1015 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("AreaName_lang")]
        public string AreaNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_table")]
    public sealed record AreaTableHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ZoneName")]
        public string ZoneName;

        [DBFieldName("AreaName")]
        public string AreaName;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("ParentAreaID")]
        public ushort? ParentAreaID;

        [DBFieldName("AreaBit")]
        public short? AreaBit;

        [DBFieldName("SoundProviderPref")]
        public byte? SoundProviderPref;

        [DBFieldName("SoundProviderPrefUnderwater")]
        public byte? SoundProviderPrefUnderwater;

        [DBFieldName("AmbienceID")]
        public ushort? AmbienceID;

        [DBFieldName("UwAmbience")]
        public ushort? UwAmbience;

        [DBFieldName("ZoneMusic")]
        public ushort? ZoneMusic;

        [DBFieldName("UwZoneMusic")]
        public ushort? UwZoneMusic;

        [DBFieldName("ExplorationLevel")]
        public sbyte? ExplorationLevel;

        [DBFieldName("IntroSound")]
        public ushort? IntroSound;

        [DBFieldName("UwIntroSound")]
        public uint? UwIntroSound;

        [DBFieldName("FactionGroupMask")]
        public byte? FactionGroupMask;

        [DBFieldName("AmbientMultiplier")]
        public float? AmbientMultiplier;

        [DBFieldName("MountFlags")]
        public byte? MountFlags;

        [DBFieldName("PvpCombatWorldStateID")]
        public short? PvpCombatWorldStateID;

        [DBFieldName("WildBattlePetLevelMin")]
        public byte? WildBattlePetLevelMin;

        [DBFieldName("WildBattlePetLevelMax")]
        public byte? WildBattlePetLevelMax;

        [DBFieldName("WindSettingsID")]
        public byte? WindSettingsID;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("LiquidTypeID", 4)]
        public ushort?[] LiquidTypeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_table_locale")]
    public sealed record AreaTableLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("AreaName_lang")]
        public string AreaNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("area_table")]
    public sealed record AreaTableHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ZoneName")]
        public string ZoneName;

        [DBFieldName("AreaName")]
        public string AreaName;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("ParentAreaID")]
        public ushort? ParentAreaID;

        [DBFieldName("AreaBit")]
        public short? AreaBit;

        [DBFieldName("SoundProviderPref")]
        public byte? SoundProviderPref;

        [DBFieldName("SoundProviderPrefUnderwater")]
        public byte? SoundProviderPrefUnderwater;

        [DBFieldName("AmbienceID")]
        public ushort? AmbienceID;

        [DBFieldName("UwAmbience")]
        public ushort? UwAmbience;

        [DBFieldName("ZoneMusic")]
        public ushort? ZoneMusic;

        [DBFieldName("UwZoneMusic")]
        public ushort? UwZoneMusic;

        [DBFieldName("ExplorationLevel")]
        public sbyte? ExplorationLevel;

        [DBFieldName("IntroSound")]
        public ushort? IntroSound;

        [DBFieldName("UwIntroSound")]
        public uint? UwIntroSound;

        [DBFieldName("FactionGroupMask")]
        public byte? FactionGroupMask;

        [DBFieldName("AmbientMultiplier")]
        public float? AmbientMultiplier;

        [DBFieldName("MountFlags")]
        public int? MountFlags;

        [DBFieldName("PvpCombatWorldStateID")]
        public short? PvpCombatWorldStateID;

        [DBFieldName("WildBattlePetLevelMin")]
        public byte? WildBattlePetLevelMin;

        [DBFieldName("WildBattlePetLevelMax")]
        public byte? WildBattlePetLevelMax;

        [DBFieldName("WindSettingsID")]
        public byte? WindSettingsID;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("LiquidTypeID", 4)]
        public ushort?[] LiquidTypeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_table_locale")]
    public sealed record AreaTableLocaleHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("AreaName_lang")]
        public string AreaNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
