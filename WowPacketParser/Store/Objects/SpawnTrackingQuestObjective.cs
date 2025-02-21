using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spawn_tracking_quest_objective")]
    public sealed record SpawnTrackingQuestObjective : IDataModel
    {
        [DBFieldName("SpawnTrackingId", true)]
        public uint? SpawnTrackingId;

        [DBFieldName("QuestObjectiveId", true)]
        public uint? QuestObjectiveId;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
