using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("areatrigger_create_properties_orbit")]
    public sealed record AreaTriggerCreatePropertiesOrbit : IDataModel
    {
        [DBFieldName("AreaTriggerCreatePropertiesId", true, true)]
        public uint? AreaTriggerCreatePropertiesId;

        [DBFieldName("IsCustom", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic, true)]
        public byte? IsCustom;

        [DBFieldName("StartDelay")]
        public uint? StartDelay;

        [DBFieldName("CircleRadius")]
        public float? CircleRadius;

        [DBFieldName("BlendFromRadius")]
        public float? BlendFromRadius;

        [DBFieldName("InitialAngle")]
        public float? InitialAngle;

        [DBFieldName("ZOffset")]
        public float? ZOffset;

        [DBFieldName("CounterClockwise")]
        public bool? CounterClockwise;

        [DBFieldName("CanLoop")]
        public bool? CanLoop;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        // Will be inserted as comment
        public uint spellId = 0;

        public WowGuid areatriggerGuid;
    }
}
