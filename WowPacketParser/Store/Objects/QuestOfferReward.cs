using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_offer_reward")]
    public sealed record QuestOfferReward : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Emote", 4)]
        public int?[] Emote;

        [DBFieldName("EmoteDelay", 4)]
        public uint?[] EmoteDelay;

        [DBFieldName("RewardText", LocaleConstant.enUS)]
        public string RewardText;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
