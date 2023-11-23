using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transmog_set_item")]
    public sealed record TransmogSetItemHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TransmogSetID")]
        public uint? TransmogSetID;

        [DBFieldName("ItemModifiedAppearanceID")]
        public uint? ItemModifiedAppearanceID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("transmog_set_item")]
    public sealed record TransmogSetItemHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TransmogSetID")]
        public uint? TransmogSetID;

        [DBFieldName("ItemModifiedAppearanceID")]
        public uint? ItemModifiedAppearanceID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
