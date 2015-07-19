using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("area_poi_state")]
    public sealed class AreaPOIState
    {
        [DBFieldName("AreaPOIID")]
        public uint AreaPOIID;

        [DBFieldName("State")]
        public uint State;

        [DBFieldName("Icon")]
        public uint Icon;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
