using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("taxi_path")]
    public sealed class TaxiPath : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("From")]
        public uint? From;

        [DBFieldName("To")]
        public uint? To;

        [DBFieldName("Cost")]
        public uint? Cost;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
