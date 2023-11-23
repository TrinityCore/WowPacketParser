using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("cfg_regions")]
    public sealed record CfgRegionsHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Tag")]
        public string Tag;

        [DBFieldName("RegionID")]
        public ushort? RegionID;

        [DBFieldName("Raidorigin")]
        public uint? Raidorigin;

        [DBFieldName("RegionGroupMask")]
        public byte? RegionGroupMask;

        [DBFieldName("ChallengeOrigin")]
        public uint? ChallengeOrigin;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("cfg_regions")]
    public sealed record CfgRegionsHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Tag")]
        public string Tag;

        [DBFieldName("RegionID")]
        public ushort? RegionID;

        [DBFieldName("Raidorigin")]
        public uint? Raidorigin;

        [DBFieldName("RegionGroupMask")]
        public byte? RegionGroupMask;

        [DBFieldName("ChallengeOrigin")]
        public uint? ChallengeOrigin;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
