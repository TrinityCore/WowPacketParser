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

        [DBFieldName("spawnMask", TargetedDatabase.Zero, TargetedDatabase.Legion)]
        public uint? SpawnMask;

        [DBFieldName("spawnDifficulties", TargetedDatabase.Legion)]
        public string spawnDifficulties;

        [DBFieldName("phaseMask", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? PhaseMask;

        [DBFieldName("PhaseId", TargetedDatabase.Cataclysm)]
        public string PhaseID;

        [DBFieldName("PhaseGroup", TargetedDatabase.Cataclysm)]
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

        [DBFieldName("curhealth")]
        public uint? CurHealth;

        [DBFieldName("curmana")]
        public uint? CurMana;

        [DBFieldName("MovementType")]
        public uint? MovementType;

        [DBFieldName("npcflag")]
        public uint? NpcFlag;

        [DBFieldName("unit_flags")]
        public uint? UnitFlag;

        [DBFieldName("dynamicflags")]
        public uint? DynamicFlag;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
