using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_trainer")]
    public sealed record CreatureTrainer : IDataModel
    {
        [DBFieldName("CreatureID", true)]
        public uint? CreatureId;

        [DBFieldName("TrainerID")]
        public uint? TrainerId;

        [DBFieldName("MenuID", true)]
        public uint? MenuID;

        [DBFieldName("OptionID", true)]
        public uint? OptionIndex;
    }
}
