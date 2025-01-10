using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spawn_tracking")]
    public sealed record SpawnTracking : IDataModel
    {
        [DBFieldName("SpawnTrackingId", true)]
        public uint? SpawnTrackingId;

        [DBFieldName("SpawnType", true)]
        public byte? SpawnType;

        [DBFieldName("SpawnId", true, true)]
        public string SpawnId;

        [DBFieldName("QuestObjectiveId")]
        public uint? QuestObjectiveId;
    }
}
