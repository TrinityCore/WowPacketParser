using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_poi_points")]
    public sealed class QuestPOIPoint : IDataModel
    {
        [DBFieldName("QuestID", true)]
        public int? QuestID;

        [DBFieldName("Idx1", true)]
        public int? Idx1;

        [DBFieldName("Idx2", true)]
        public int? Idx2;

        [DBFieldName("X")]
        public int? X;

        [DBFieldName("Y")]
        public int? Y;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
