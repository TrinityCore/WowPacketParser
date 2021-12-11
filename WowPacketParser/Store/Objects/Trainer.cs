using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("trainer")]
    public sealed record Trainer : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? Id;

        [DBFieldName("Type")]
        public TrainerType? Type;

        [DBFieldName("Greeting")]
        public string Greeting;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
