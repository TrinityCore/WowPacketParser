using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("areatrigger_template")]
    public sealed record AreaTriggerTemplate : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? Id;

        [DBFieldName("IsServerSide", TargetedDatabase.Shadowlands, true)]
        public byte? IsServerSide = 0;

        [DBFieldName("Type")] // kept in TargetedDatabase.Shadowlands to preserve data for non-spell areatriggers
        public byte? Type;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("Data", 8, true)] // kept in TargetedDatabase.Shadowlands to preserve data for non-spell areatriggers
        public float?[] Data = { 0, 0, 0, 0, 0, 0, 0, 0 };

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
