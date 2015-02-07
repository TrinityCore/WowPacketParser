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
        [DBFieldName("Type")]
        public uint Type;

        [DBFieldName("GreetEmoteType")]
        public uint GreetEmoteType;

        [DBFieldName("GreetEmoteDelay")]
        public uint GreetEmoteDelay;

        [DBFieldName("Greeting")]
        public string Greeting;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("quest_offer_reward")]
    public class QuestOfferReward
    {
        [DBFieldName("Emote", 4)]
        public uint[] Emote;

        [DBFieldName("EmoteDelay", 4)]
        public uint[] EmoteDelay;

        [DBFieldName("RewardText")]
        public string RewardText;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("quest_details")]
    public class QuestDetails
    {
        [DBFieldName("Emote", 4)]
        public uint[] Emote;

        [DBFieldName("EmoteDelay", 4)]
        public uint[] EmoteDelay;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("quest_request_items")]
    public class QuestRequestItems
    {
        [DBFieldName("CompEmoteType")]
        public int CompEmoteType;

        [DBFieldName("CompEmoteDelay")]
        public int CompEmoteDelay;

        [DBFieldName("CompletionText")]
        public string CompletionText;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
