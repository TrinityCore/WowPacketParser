using WowPacketParser.Enums;
//using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    //[DBTableName("points_of_interest")]
    public sealed class GossipPOI
    {
        //[DBFieldName("x")]
        public float XPos;

        //[DBFieldName("y")]
        public float YPos;

        //[DBFieldName("icon")]
        public GossipPOIIcon Icon;

        //[DBFieldName("flags")]
        public uint Flags;

        //[DBFieldName("data")]
        public uint Data;

        //[DBFieldName("icon_name")]
        public string IconName;
    }
}
