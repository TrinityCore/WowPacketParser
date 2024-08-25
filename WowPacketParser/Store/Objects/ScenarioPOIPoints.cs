using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("scenario_poi_points")]
    public sealed record ScenarioPOIPoint : IDataModel
    {
        [DBFieldName("CriteriaTreeID", true)]
        public int? CriteriaTreeID;

        [DBFieldName("Idx1", true)]
        public uint? Idx1;

        [DBFieldName("Idx2", true)]
        public uint? Idx2;

        [DBFieldName("X")]
        public int? X;

        [DBFieldName("Y")]
        public int? Y;

        [DBFieldName("Z")]
        public int? Z;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}