using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_range")]
    public sealed record SpellRangeHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DisplayName")]
        public string DisplayName;

        [DBFieldName("DisplayNameShort")]
        public string DisplayNameShort;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("RangeMin", 2)]
        public float?[] RangeMin;

        [DBFieldName("RangeMax", 2)]
        public float?[] RangeMax;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_range_locale")]
    public sealed record SpellRangeLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("DisplayName_lang")]
        public string DisplayNameLang;

        [DBFieldName("DisplayNameShort_lang")]
        public string DisplayNameShortLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_range")]
    public sealed record SpellRangeHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DisplayName")]
        public string DisplayName;

        [DBFieldName("DisplayNameShort")]
        public string DisplayNameShort;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("RangeMin", 2)]
        public float?[] RangeMin;

        [DBFieldName("RangeMax", 2)]
        public float?[] RangeMax;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_range_locale")]
    public sealed record SpellRangeLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("DisplayName_lang")]
        public string DisplayNameLang;

        [DBFieldName("DisplayNameShort_lang")]
        public string DisplayNameShortLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
