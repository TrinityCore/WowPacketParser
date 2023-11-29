using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject")]
    public sealed record GameObjectModel : IDataModel
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

        [DBFieldName("spawnMask", TargetedDatabaseFlag.TillBattleForAzeroth)]
        public uint? SpawnMask;

        [DBFieldName("spawnDifficulties", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public string spawnDifficulties;

        [DBFieldName("phaseMask", TargetedDatabaseFlag.WrathOfTheLichKing)]
        public uint? PhaseMask;

        [DBFieldName("PhaseId", TargetedDatabaseFlag.SinceCataclysm | TargetedDatabaseFlag.WotlkClassic)]
        public string PhaseID;

        [DBFieldName("PhaseGroup", TargetedDatabaseFlag.SinceCataclysm | TargetedDatabaseFlag.WotlkClassic)]
        public uint? PhaseGroup;

        [DBFieldName("position_x")]
        public float? PositionX;

        [DBFieldName("position_y")]
        public float? PositionY;

        [DBFieldName("position_z")]
        public float? PositionZ;

        [DBFieldName("orientation")]
        public float? Orientation;

        [DBFieldName("rotation", 4, true)]
        public float?[] Rotation;

        [DBFieldName("spawntimesecs")]
        public int? SpawnTimeSecs;

        [DBFieldName("animprogress")]
        public uint? AnimProgress;

        [DBFieldName("state")]
        public uint? State;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
