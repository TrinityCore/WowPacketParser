using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("areatrigger_template")]
    public sealed class AreaTriggerTemplate : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? Id;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("Data", 6, true)]
        public float?[] Data = { 0, 0, 0, 0, 0, 0 };

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
