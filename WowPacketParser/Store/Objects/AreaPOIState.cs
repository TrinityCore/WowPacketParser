using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("area_poi_state")]
    public sealed class AreaPOIState : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AreaPOIID")]
        public uint? AreaPoiID;

        [DBFieldName("State")]
        public uint? State;

        [DBFieldName("Icon")]
        public uint? Icon;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
