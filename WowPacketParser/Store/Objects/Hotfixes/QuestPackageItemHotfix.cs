using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("quest_package_item")]
    public sealed record QuestPackageItemHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PackageID")]
        public ushort? PackageID;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("ItemQuantity")]
        public uint? ItemQuantity;

        [DBFieldName("DisplayType")]
        public byte? DisplayType;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("quest_package_item")]
    public sealed record QuestPackageItemHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PackageID")]
        public ushort? PackageID;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("ItemQuantity")]
        public uint? ItemQuantity;

        [DBFieldName("DisplayType")]
        public byte? DisplayType;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
