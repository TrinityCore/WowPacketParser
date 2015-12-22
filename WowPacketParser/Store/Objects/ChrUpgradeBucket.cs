using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("chr_upgrade_bucket")]
    public sealed class ChrUpgradeBucket : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TierID")]
        public uint? TierID;

        [DBFieldName("SpecializationID")]
        public uint? SpecializationID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
