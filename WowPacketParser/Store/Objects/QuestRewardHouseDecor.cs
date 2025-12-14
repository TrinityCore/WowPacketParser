using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_reward_house_decor")]
    public class QuestRewardHouseDecor : IDataModel
    {
        [DBFieldName("QuestID", true)]
        public uint? QuestID;

        [DBFieldName("HouseDecorID", true)]
        public int? HouseDecorID;

        [DBFieldName("OrderIndex", true)]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
