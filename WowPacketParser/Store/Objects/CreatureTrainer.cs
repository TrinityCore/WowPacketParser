using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_trainer")]
    public class CreatureTrainer : IDataModel
    {
        [DBFieldName("CreatureId", true)]
        public uint? CreatureId;

        [DBFieldName("TrainerId")]
        public uint? TrainerId;

        [DBFieldName("MenuID", true)]
        public uint? MenuID;

        [DBFieldName("OptionIndex", true)]
        public uint? OptionIndex;
    }
}
