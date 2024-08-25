using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("scenario_poi")]
    public sealed record ScenarioPOI : IDataModel
    {
        [DBFieldName("CriteriaTreeID", true)]
        public int? CriteriaTreeID;

        [DBFieldName("BlobIndex", true)]
        public int? BlobIndex;

        [DBFieldName("Idx1", true)]
        public uint? Idx1;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("WorldMapAreaId", TargetedDatabaseFlag.TillLegion)]
        [DBFieldName("UiMapID", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public int? WorldMapAreaId;

        [DBFieldName("Floor", TargetedDatabaseFlag.TillLegion)]
        public int? Floor;

        [DBFieldName("Priority")]
        public int? Priority;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("WorldEffectID")]
        public int? WorldEffectID;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("NavigationPlayerConditionID")]
        public int? NavigationPlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}