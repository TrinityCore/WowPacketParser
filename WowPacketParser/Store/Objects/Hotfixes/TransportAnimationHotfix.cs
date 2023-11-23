using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transport_animation")]
    public sealed record TransportAnimationHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("SequenceID")]
        public byte? SequenceID;

        [DBFieldName("TimeIndex")]
        public uint? TimeIndex;

        [DBFieldName("TransportID")]
        public uint? TransportID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("transport_animation")]
    public sealed record TransportAnimationHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("SequenceID")]
        public byte? SequenceID;

        [DBFieldName("TimeIndex")]
        public uint? TimeIndex;

        [DBFieldName("TransportID")]
        public int? TransportID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
