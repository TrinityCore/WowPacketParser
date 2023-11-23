using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("azerite_essence_power")]
    public sealed record AzeriteEssencePowerHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SourceAlliance")]
        public string SourceAlliance;

        [DBFieldName("SourceHorde")]
        public string SourceHorde;

        [DBFieldName("AzeriteEssenceID")]
        public int? AzeriteEssenceID;

        [DBFieldName("Tier")]
        public byte? Tier;

        [DBFieldName("MajorPowerDescription")]
        public int? MajorPowerDescription;

        [DBFieldName("MinorPowerDescription")]
        public int? MinorPowerDescription;

        [DBFieldName("MajorPowerActual")]
        public int? MajorPowerActual;

        [DBFieldName("MinorPowerActual")]
        public int? MinorPowerActual;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("azerite_essence_power_locale")]
    public sealed record AzeriteEssencePowerLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("SourceAlliance_lang")]
        public string SourceAllianceLang;

        [DBFieldName("SourceHorde_lang")]
        public string SourceHordeLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("azerite_essence_power")]
    public sealed record AzeriteEssencePowerHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SourceAlliance")]
        public string SourceAlliance;

        [DBFieldName("SourceHorde")]
        public string SourceHorde;

        [DBFieldName("AzeriteEssenceID")]
        public int? AzeriteEssenceID;

        [DBFieldName("Tier")]
        public byte? Tier;

        [DBFieldName("MajorPowerDescription")]
        public int? MajorPowerDescription;

        [DBFieldName("MinorPowerDescription")]
        public int? MinorPowerDescription;

        [DBFieldName("MajorPowerActual")]
        public int? MajorPowerActual;

        [DBFieldName("MinorPowerActual")]
        public int? MinorPowerActual;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("azerite_essence_power_locale")]
    public sealed record AzeriteEssencePowerLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("SourceAlliance_lang")]
        public string SourceAllianceLang;

        [DBFieldName("SourceHorde_lang")]
        public string SourceHordeLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
