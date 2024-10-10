using System.Collections.Generic;
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

        [DBFieldName("AreaTriggerCreatePropertiesId", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic, false, true)]
        [DBFieldName("AreaTriggerId", TargetedDatabaseFlag.TillShadowlands, false, true)]
        public string AreaTriggerCreatePropertiesId;

        [DBFieldName("IsCustom", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic)]
        [DBFieldName("IsServerSide", TargetedDatabaseFlag.TillShadowlands)]
        public byte? IsCustom;

        [DBFieldName("MapId")]
        public uint? MapId;

        [DBFieldName("SpawnDifficulties", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic)]
        public string SpawnDifficulties;

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

        [DBFieldName("Shape", TargetedDatabaseFlag.TillShadowlands)]
        public byte? Shape;

        [DBFieldName("ShapeData", TargetedDatabaseFlag.TillShadowlands, 8, true)] // kept in TargetedDatabase.Shadowlands to preserve data for non-spell areatriggers
        public float?[] ShapeData = { 0, 0, 0, 0, 0, 0, 0, 0 };

        [DBFieldName("SpellForVisuals", TargetedDatabaseFlag.TillShadowlands, false, false, true)]
        public uint? SpellForVisuals;

        [DBFieldName("Comment")]
        public string Comment;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public WowGuid areatriggerGuid;

        public List<int> GetDefaultSpawnDifficulties()
        {
            if (Settings.UseDBC && DBC.DBC.MapDifficultyStores != null)
            {
                if (DBC.DBC.MapDifficultyStores.ContainsKey((int)MapId))
                    return DBC.DBC.MapDifficultyStores[(int)MapId];
            }

            return new List<int>();
        }
    }
}
