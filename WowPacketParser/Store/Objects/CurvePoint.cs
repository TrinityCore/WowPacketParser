using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("curve_point")]
    public sealed class CurvePoint : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CurveID")]
        public uint? CurveID;

        [DBFieldName("Index")]
        public uint? Index;

        [DBFieldName("X")]
        public float? X;

        [DBFieldName("Y")]
        public float? Y;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
