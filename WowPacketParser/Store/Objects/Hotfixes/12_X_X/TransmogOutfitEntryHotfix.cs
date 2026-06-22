using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transmog_outfit_entry")]
    public sealed record TransmogOutfitEntryHotfix1200: IDataModel
    {
        [DBFieldName("Cost")]
        public ulong? Cost;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("Source")]
        public byte? Source;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SetType")]
        public byte? SetType;

        [DBFieldName("OverrideCostModifier")]
        public float? OverrideCostModifier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("transmog_outfit_entry_locale")]
    public sealed record TransmogOutfitEntryLocaleHotfix1200: IDataModel
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
    [DBTableName("transmog_outfit_entry")]
    public sealed record TransmogOutfitEntryHotfix1205: IDataModel
    {
        [DBFieldName("Cost")]
        public ulong? Cost;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("Source")]
        public byte? Source;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SetType")]
        public byte? SetType;

        [DBFieldName("OverrideCostModifier")]
        public float? OverrideCostModifier;

        [DBFieldName("OutfitIndex")]
        public int? OutfitIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("transmog_outfit_entry_locale")]
    public sealed record TransmogOutfitEntryLocaleHotfix1205: IDataModel
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
