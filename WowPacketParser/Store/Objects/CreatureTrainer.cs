using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_trainer")]
    public sealed record CreatureTrainer : IDataModel
    {
        [DBFieldName("CreatureID", true)]
        public uint? CreatureID;

        [DBFieldName("TrainerID")]
        public uint? TrainerID;

        [DBFieldName("MenuID", true)]
        public uint? MenuID;

        [DBFieldName("OptionID", true)]
        public uint? OptionID;
    }
}
