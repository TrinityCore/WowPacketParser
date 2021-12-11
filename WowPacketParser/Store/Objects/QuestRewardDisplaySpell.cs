using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_reward_display_spell")]
    public sealed record QuestRewardDisplaySpell : IDataModel
    {
        [DBFieldName("QuestID", true)]
        public uint? QuestID;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}