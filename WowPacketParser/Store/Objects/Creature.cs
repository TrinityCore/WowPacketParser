using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature")]
    public sealed record Creature : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("id")]
        public uint? ID;

        [DBFieldName("map")]
        public uint? Map;

        [DBFieldName("zoneId")]
        public uint? ZoneID;

        [DBFieldName("areaId")]
        public uint? AreaID;

        [DBFieldName("spawnMask", TargetedDatabaseFlag.TillWarlordsOfDraenor)]
        public uint? SpawnMask;

        [DBFieldName("spawnDifficulties", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.AnyClassic)]
        public string spawnDifficulties;

        [DBFieldName("phaseMask", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? PhaseMask;

        [DBFieldName("PhaseId", TargetedDatabaseFlag.SinceCataclysm | TargetedDatabaseFlag.AnyClassic)]
        public string PhaseID;

        [DBFieldName("PhaseGroup", TargetedDatabaseFlag.SinceCataclysm | TargetedDatabaseFlag.AnyClassic)]
        public int? PhaseGroup;

        [DBFieldName("modelid")]
        public uint? ModelID;

        [DBFieldName("equipment_id")]
        public int EquipmentID;

        [DBFieldName("position_x")]
        public float? PositionX;

        [DBFieldName("position_y")]
        public float? PositionY;

        [DBFieldName("position_z")]
        public float? PositionZ;

        [DBFieldName("orientation")]
        public float? Orientation;

        [DBFieldName("spawntimesecs")]
        public int? SpawnTimeSecs;

        [DBFieldName("wander_distance")]
        public float? WanderDistance;

        [DBFieldName("currentwaypoint")]
        public uint? CurrentWaypoint;

        [DBFieldName("curhealth", TargetedDatabaseFlag.TillShadowlands)]
        public uint? CurHealth;

        [DBFieldName("curHealthPct", TargetedDatabaseFlag.Dragonflight | TargetedDatabaseFlag.WotlkClassic)]
        public uint? CurHealthPct;

        [DBFieldName("curmana", TargetedDatabaseFlag.TillShadowlands)]
        public uint? CurMana;

        [DBFieldName("MovementType")]
        public uint? MovementType;

        [DBFieldName("npcflag", false, false, true)]
        public uint? NpcFlag;

        [DBFieldName("unit_flags", false, false, true)]
        public uint? UnitFlags;

        [DBFieldName("unit_flags2", false, false, true)]
        public uint? UnitFlags2;

        [DBFieldName("unit_flags3", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.AnyClassic, false, false, true)]
        public uint? UnitFlags3;

        [DBFieldName("dynamicflags", TargetedDatabaseFlag.TillShadowlands | TargetedDatabaseFlag.AnyClassic)]
        public uint? DynamicFlag;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
