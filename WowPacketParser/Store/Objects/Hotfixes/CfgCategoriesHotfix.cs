using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("cfg_categories")]
    public sealed record CfgCategoriesHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("LocaleMask")]
        public ushort? LocaleMask;

        [DBFieldName("CreateCharsetMask")]
        public byte? CreateCharsetMask;

        [DBFieldName("ExistingCharsetMask")]
        public byte? ExistingCharsetMask;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("Order")]
        public sbyte? Order;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
