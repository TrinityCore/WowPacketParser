using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("unit_condition")]
    public sealed record UnitConditionHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("Variable", 8)]
        public byte?[] Variable;

        [DBFieldName("Op", 8)]
        public sbyte?[] Op;

        [DBFieldName("Value", 8)]
        public int?[] Value;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("unit_condition")]
    public sealed record UnitConditionHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("Variable", 8)]
        public byte?[] Variable;

        [DBFieldName("Op", 8)]
        public sbyte?[] Op;

        [DBFieldName("Value", 8)]
        public int?[] Value;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
