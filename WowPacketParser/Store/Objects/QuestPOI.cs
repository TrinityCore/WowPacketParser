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

        [DBFieldName("BlobIndex", TargetedDatabaseFlag.SinceWarlordsOfDraenor | TargetedDatabaseFlag.WotlkClassic, true)]
        public int? BlobIndex;

        [DBFieldName("id", TargetedDatabaseFlag.TillCataclysm, true)]
        [DBFieldName("Idx1", TargetedDatabaseFlag.SinceWarlordsOfDraenor | TargetedDatabaseFlag.WotlkClassic, true)]
        public int? ID;

        [DBFieldName("ObjectiveIndex")]
        public int? ObjectiveIndex;

        [DBFieldName("QuestObjectiveID", TargetedDatabaseFlag.SinceWarlordsOfDraenor | TargetedDatabaseFlag.WotlkClassic)]
        public int? QuestObjectiveID;

        [DBFieldName("QuestObjectID", TargetedDatabaseFlag.SinceWarlordsOfDraenor | TargetedDatabaseFlag.WotlkClassic)]
        public int? QuestObjectID;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("UiMapID", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.WotlkClassic)]
        public int? UiMapID;

        [DBFieldName("WorldMapAreaId", TargetedDatabaseFlag.TillLegion)]
        public int? WorldMapAreaId;

        [DBFieldName("Floor", TargetedDatabaseFlag.TillLegion)]
        public int? Floor;

        [DBFieldName("Priority")]
        public int? Priority;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("WorldEffectID", TargetedDatabaseFlag.SinceWarlordsOfDraenor | TargetedDatabaseFlag.WotlkClassic)]
        public int? WorldEffectID;

        [DBFieldName("PlayerConditionID", TargetedDatabaseFlag.SinceWarlordsOfDraenor | TargetedDatabaseFlag.WotlkClassic)]
        public int? PlayerConditionID;

        [DBFieldName("NavigationPlayerConditionID", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic)]
        public int? NavigationPlayerConditionID;

        [DBFieldName("SpawnTrackingID", TargetedDatabaseFlag.SinceWarlordsOfDraenor | TargetedDatabaseFlag.WotlkClassic)]
        public int? SpawnTrackingID;

        [DBFieldName("AlwaysAllowMergingBlobs", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public bool? AlwaysAllowMergingBlobs;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
