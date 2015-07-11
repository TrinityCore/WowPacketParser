using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_poi_points")]
    public class QuestPOIPoint
    {
        [DBFieldName("idx")]
        public uint Index; // Client expects a certain order although this is not on sniffs

        [DBFieldName("x")]
        public int X;

        [DBFieldName("y")]
        public int Y;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("quest_poi_points")]
    public class QuestPOIPointWoD
    {
        [DBFieldName("X")]
        public int X;

        [DBFieldName("Y")]
        public int Y;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
