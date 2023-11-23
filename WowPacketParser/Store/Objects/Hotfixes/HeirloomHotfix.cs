using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("heirloom")]
    public sealed record HeirloomHotfix1000: IDataModel
    {
        [DBFieldName("SourceText")]
        public string SourceText;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("LegacyUpgradedItemID")]
        public int? LegacyUpgradedItemID;

        [DBFieldName("StaticUpgradedItemID")]
        public int? StaticUpgradedItemID;

        [DBFieldName("SourceTypeEnum")]
        public sbyte? SourceTypeEnum;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("LegacyItemID")]
        public int? LegacyItemID;

        [DBFieldName("UpgradeItemID", 6)]
        public int?[] UpgradeItemID;

        [DBFieldName("UpgradeItemBonusListID", 6)]
        public ushort?[] UpgradeItemBonusListID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("heirloom_locale")]
    public sealed record HeirloomLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("SourceText_lang")]
        public string SourceTextLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("heirloom")]
    public sealed record HeirloomHotfix340: IDataModel
    {
        [DBFieldName("SourceText")]
        public string SourceText;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("LegacyUpgradedItemID")]
        public int? LegacyUpgradedItemID;

        [DBFieldName("StaticUpgradedItemID")]
        public int? StaticUpgradedItemID;

        [DBFieldName("SourceTypeEnum")]
        public sbyte? SourceTypeEnum;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("LegacyItemID")]
        public int? LegacyItemID;

        [DBFieldName("UpgradeItemID", 6)]
        public int?[] UpgradeItemID;

        [DBFieldName("UpgradeItemBonusListID", 6)]
        public ushort?[] UpgradeItemBonusListID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("heirloom_locale")]
    public sealed record HeirloomLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("SourceText_lang")]
        public string SourceTextLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
