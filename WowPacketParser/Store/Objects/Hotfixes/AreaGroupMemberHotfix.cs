using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("area_group_member")]
    public sealed record AreaGroupMemberHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AreaID")]
        public ushort? AreaID;

        [DBFieldName("AreaGroupID")]
        public uint? AreaGroupID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_group_member")]
    public sealed record AreaGroupMemberHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AreaID")]
        public ushort? AreaID;

        [DBFieldName("AreaGroupID")]
        public int? AreaGroupID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
