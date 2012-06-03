using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_template")]
    public class QuestOffer
    {
        [DBFieldName("OfferRewardText")]
        public string OfferRewardText;
    }

    [DBTableName("quest_template")]
    public class QuestReward
    {
        [DBFieldName("RequestItemsText")]
        public string RequestItemsText;
    }
}
