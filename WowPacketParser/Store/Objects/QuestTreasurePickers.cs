using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_treasure_pickers")]
    public sealed record QuestTreasurePickers : IDataModel
    {
        [DBFieldName("QuestID", true)]
        public uint? QuestID;

        [DBFieldName("TreasurePickerID", true)]
        public int? TreasurePickerID;

        [DBFieldName("OrderIndex", true)]
        public int? OrderIndex;
    }
}
