using WowPacketParser.Enums;
using WowPacketParser.Misc;
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

    [DBTableName("quest_greeting")]
    public class QuestGreeting
    {
        [DBFieldName("ObjectType")]
        public ObjectType ObjectType;

        [DBFieldName("GreetEmoteType")]
        public uint GreetEmoteType;

        [DBFieldName("GreetEmoteDelay")]
        public uint GreetEmoteDelay;

        [DBFieldName("Greeting")]
        public string Greeting;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
