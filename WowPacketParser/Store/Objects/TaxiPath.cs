using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("taxi_path")]
    public sealed class TaxiPath
    {
        [DBFieldName("From")]
        public uint From;

        [DBFieldName("To")]
        public uint To;

        [DBFieldName("Cost")]
        public uint Cost;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
