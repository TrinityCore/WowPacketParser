using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("trainer")]
    public sealed class Trainer : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? Id;

        [DBFieldName("Type")]
        public TrainerType? Type;

        [DBFieldName("Greeting")]
        public string Greeting;
    }
}
