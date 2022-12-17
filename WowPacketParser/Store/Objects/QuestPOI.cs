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

        [DBFieldName("BlobIndex", TargetedDatabaseFlag.SinceWarlordsOfDraenor, true)]
        public int? BlobIndex;

        [DBFieldName("id", TargetedDatabaseFlag.TillCataclysm, true)]
        [DBFieldName("Idx1", TargetedDatabaseFlag.SinceWarlordsOfDraenor, true)]
        public int? ID;

        [DBFieldName("ObjectiveIndex")]
        public int? ObjectiveIndex;

        [DBFieldName("QuestObjectiveID", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public int? QuestObjectiveID;

        [DBFieldName("QuestObjectID", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public int? QuestObjectID;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("UiMapID", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public int? UiMapID;

        [DBFieldName("WorldMapAreaId", TargetedDatabaseFlag.TillLegion)]
        public int? WorldMapAreaId;

        [DBFieldName("Floor", TargetedDatabaseFlag.TillLegion)]
        public int? Floor;

        [DBFieldName("Priority")]
        public int? Priority;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("WorldEffectID", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public int? WorldEffectID;

        [DBFieldName("PlayerConditionID", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public int? PlayerConditionID;

        [DBFieldName("NavigationPlayerConditionID", TargetedDatabaseFlag.SinceShadowlands)]
        public int? NavigationPlayerConditionID;

        [DBFieldName("SpawnTrackingID", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public int? SpawnTrackingID;

        [DBFieldName("AlwaysAllowMergingBlobs", TargetedDatabaseFlag.SinceLegion)]
        public bool? AlwaysAllowMergingBlobs;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
