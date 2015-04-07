using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("location")]
    public sealed class Location
    {
        [DBFieldName("LocX")]
        public float LocX;

        [DBFieldName("LocX")]
        public float LocY;

        [DBFieldName("LocX")]
        public float LocZ;

        [DBFieldName("Rotation", 3)]
        public float[] Rotation;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
