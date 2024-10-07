using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("paragon_reputation")]
    public sealed record ParagonReputationHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FactionID")]
        public int? FactionID;

        [DBFieldName("LevelThreshold")]
        public int? LevelThreshold;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
