using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("points_of_interest")]
    public sealed class PointsOfInterest : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PositionX")]
        public float? PositionX;

        [DBFieldName("PositionY")]
        public float? PositionY;

        [DBFieldName("Icon")]
        public GossipPOIIcon? Icon;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("Importance")]
        public uint? Importance;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
