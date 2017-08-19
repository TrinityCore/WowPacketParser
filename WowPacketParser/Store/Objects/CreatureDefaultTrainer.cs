using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_default_trainer")]
    public class CreatureDefaultTrainer : IDataModel
    {
        [DBFieldName("CreatureId", true)]
        public uint? CreatureId;

        [DBFieldName("TrainerId")]
        public uint? TrainerId;
    }
}
