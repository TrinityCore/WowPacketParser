using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_reward_house_room")]
    public class QuestRewardHouseRoom : IDataModel
    {
        [DBFieldName("QuestID", true)]
        public uint? QuestID;

        [DBFieldName("HouseRoomID", true)]
        public int? HouseRoomID;

        [DBFieldName("OrderIndex", true)]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
