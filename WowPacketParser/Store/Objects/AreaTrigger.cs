using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("areatrigger")]
    public sealed record AreaTrigger : IDataModel
    {
        [DBFieldName("SpawnId", true, true)]
        public string SpawnId;

        [DBFieldName("AreaTriggerId")]
        public uint? AreaTriggerId;

        [DBFieldName("IsServerSide")]
        public byte? IsServerSide;

        [DBFieldName("MapId")]
        public uint? MapId;

        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("Orientation")]
        public float? Orientation;

        [DBFieldName("PhaseUseFlags")]
        public byte? PhaseUseFlags;

        [DBFieldName("PhaseId")]
        public string PhaseId;

        [DBFieldName("PhaseGroup")]
        public uint? PhaseGroup;

        [DBFieldName("Shape")]
        public byte? Shape;

        [DBFieldName("ShapeData", 8, true)] // kept in TargetedDatabase.Shadowlands to preserve data for non-spell areatriggers
        public float?[] ShapeData = { 0, 0, 0, 0, 0, 0, 0, 0 };

        [DBFieldName("SpellForVisuals")]
        public uint? SpellForVisuals;

        [DBFieldName("Comment")]
        public string Comment;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public WowGuid areatriggerGuid;
    }
}
