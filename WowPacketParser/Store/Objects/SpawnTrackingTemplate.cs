using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spawn_tracking_template")]
    public sealed record SpawnTrackingTemplate : IDataModel
    {
        [DBFieldName("SpawnTrackingId", true)]
        public uint? SpawnTrackingId;

        [DBFieldName("MapId")]
        public uint? MapId;

        [DBFieldName("PhaseId")]
        public int? PhaseId;

        [DBFieldName("PhaseGroup")]
        public int? PhaseGroup;

        [DBFieldName("PhaseUseFlags")]
        public byte? PhaseUseFlags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
