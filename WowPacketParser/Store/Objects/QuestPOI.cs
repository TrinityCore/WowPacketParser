using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_poi")]
    public sealed record QuestPOI : IDataModel
    {
        [DBFieldName("QuestID", true)]
        public int? QuestID;

        [DBFieldName("BlobIndex", TargetedDatabase.WarlordsOfDraenor, true)]
        public int? BlobIndex;

        [DBFieldName("id", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor, true)]
        [DBFieldName("Idx1", TargetedDatabase.WarlordsOfDraenor, true)]
        public int? ID;

        [DBFieldName("ObjectiveIndex")]
        public int? ObjectiveIndex;

        [DBFieldName("QuestObjectiveID", TargetedDatabase.WarlordsOfDraenor)]
        public int? QuestObjectiveID;

        [DBFieldName("QuestObjectID", TargetedDatabase.WarlordsOfDraenor)]
        public int? QuestObjectID;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("UiMapID", TargetedDatabase.BattleForAzeroth)]
        public int? UiMapID;

        [DBFieldName("WorldMapAreaId", TargetedDatabase.Zero, TargetedDatabase.BattleForAzeroth)]
        public int? WorldMapAreaId;

        [DBFieldName("Floor", TargetedDatabase.Zero, TargetedDatabase.BattleForAzeroth)]
        public int? Floor;

        [DBFieldName("Priority")]
        public int? Priority;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("WorldEffectID", TargetedDatabase.WarlordsOfDraenor)]
        public int? WorldEffectID;

        [DBFieldName("PlayerConditionID", TargetedDatabase.WarlordsOfDraenor)]
        public int? PlayerConditionID;

        [DBFieldName("NavigationPlayerConditionID", TargetedDatabase.Shadowlands)]
        public int? NavigationPlayerConditionID;

        [DBFieldName("SpawnTrackingID", TargetedDatabase.WarlordsOfDraenor)]
        public int? SpawnTrackingID;

        [DBFieldName("AlwaysAllowMergingBlobs", TargetedDatabase.Legion)]
        public bool? AlwaysAllowMergingBlobs;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
