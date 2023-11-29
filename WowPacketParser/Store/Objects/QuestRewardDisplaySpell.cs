using WowPacketParser.Misc;
using WowPacketParser.SQL;
using WowPacketParser.Enums;

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

        [DBFieldName("PlayerConditionID", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic)]
        public uint? PlayerConditionID;

        [DBFieldName("Type", TargetedDatabaseFlag.Dragonflight | TargetedDatabaseFlag.WotlkClassic)]
        public int? Type;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}