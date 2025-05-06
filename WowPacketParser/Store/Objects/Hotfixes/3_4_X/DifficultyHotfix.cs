using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("difficulty")]
    public sealed record DifficultyHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("InstanceType")]
        public byte? InstanceType;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("OldEnumValue")]
        public sbyte? OldEnumValue;

        [DBFieldName("FallbackDifficultyID")]
        public byte? FallbackDifficultyID;

        [DBFieldName("MinPlayers")]
        public byte? MinPlayers;

        [DBFieldName("MaxPlayers")]
        public byte? MaxPlayers;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("ToggleDifficultyID")]
        public byte? ToggleDifficultyID;

        [DBFieldName("GroupSizeHealthCurveID")]
        public ushort? GroupSizeHealthCurveID;

        [DBFieldName("GroupSizeDmgCurveID")]
        public ushort? GroupSizeDmgCurveID;

        [DBFieldName("GroupSizeSpellPointsCurveID")]
        public ushort? GroupSizeSpellPointsCurveID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("difficulty_locale")]
    public sealed record DifficultyLocaleHotfix340: IDataModel
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
    [DBTableName("difficulty")]
    public sealed record DifficultyHotfix344: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("InstanceType")]
        public byte? InstanceType;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("OldEnumValue")]
        public sbyte? OldEnumValue;

        [DBFieldName("FallbackDifficultyID")]
        public byte? FallbackDifficultyID;

        [DBFieldName("MinPlayers")]
        public byte? MinPlayers;

        [DBFieldName("MaxPlayers")]
        public byte? MaxPlayers;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("ToggleDifficultyID")]
        public byte? ToggleDifficultyID;

        [DBFieldName("GroupSizeHealthCurveID")]
        public ushort? GroupSizeHealthCurveID;

        [DBFieldName("GroupSizeDmgCurveID")]
        public ushort? GroupSizeDmgCurveID;

        [DBFieldName("GroupSizeSpellPointsCurveID")]
        public ushort? GroupSizeSpellPointsCurveID;

        [DBFieldName("Field115456400013")]
        public int? Field115456400013;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("difficulty_locale")]
    public sealed record DifficultyLocaleHotfix344: IDataModel
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
}
