using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("wmo_area_table")]
    public sealed record WmoAreaTableHotfix1000: IDataModel
    {
        [DBFieldName("AreaName")]
        public string AreaName;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("WmoID")]
        public ushort? WmoID;

        [DBFieldName("NameSetID")]
        public byte? NameSetID;

        [DBFieldName("WmoGroupID")]
        public int? WmoGroupID;

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
        public uint? UwZoneMusic;

        [DBFieldName("IntroSound")]
        public ushort? IntroSound;

        [DBFieldName("UwIntroSound")]
        public ushort? UwIntroSound;

        [DBFieldName("AreaTableID")]
        public ushort? AreaTableID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("wmo_area_table_locale")]
    public sealed record WmoAreaTableLocaleHotfix1000: IDataModel
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
    [DBTableName("wmo_area_table")]
    public sealed record WmoAreaTableHotfix340: IDataModel
    {
        [DBFieldName("AreaName")]
        public string AreaName;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("WmoID")]
        public ushort? WmoID;

        [DBFieldName("NameSetID")]
        public byte? NameSetID;

        [DBFieldName("WmoGroupID")]
        public int? WmoGroupID;

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
        public uint? UwZoneMusic;

        [DBFieldName("IntroSound")]
        public ushort? IntroSound;

        [DBFieldName("UwIntroSound")]
        public ushort? UwIntroSound;

        [DBFieldName("AreaTableID")]
        public ushort? AreaTableID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("wmo_area_table_locale")]
    public sealed record WmoAreaTableLocaleHotfix340: IDataModel
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
