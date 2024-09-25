using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    public record QuestTextConditional : IDataModel
    {
        [DBFieldName("QuestId", true)]
        public int? QuestId;

        [DBFieldName("PlayerConditionId", true)]
        public int? PlayerConditionId;

        [DBFieldName("QuestgiverCreatureId", true)]
        public int? QuestgiverCreatureId;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Text")]
        public string Text;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public QuestTextConditional(QuestTextConditional questTextConditional)
        {
            QuestId = questTextConditional.QuestId;
            PlayerConditionId = questTextConditional.PlayerConditionId;
            QuestgiverCreatureId = questTextConditional.QuestgiverCreatureId;
            Locale = ClientLocale.PacketLocaleString;
            Text = questTextConditional.Text;
            OrderIndex = questTextConditional.OrderIndex;
            VerifiedBuild = ClientVersion.BuildInt;
        }
    }

    [DBTableName("quest_description_conditional", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic)]
    public sealed record QuestDescriptionConditional : QuestTextConditional
    {
        public QuestDescriptionConditional() { }
        public QuestDescriptionConditional(QuestTextConditional questTextConditional) : base(questTextConditional) { }
    }

    [DBTableName("quest_completion_log_conditional", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic)]
    public sealed record QuestCompletionLogConditional : QuestTextConditional
    {
        public QuestCompletionLogConditional() { }
        public QuestCompletionLogConditional(QuestTextConditional questTextConditional) : base(questTextConditional) { }
    }

    [DBTableName("quest_offer_reward_conditional", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic)]
    public sealed record QuestOfferRewardConditional : QuestTextConditional
    {
        public QuestOfferRewardConditional() { }
        public QuestOfferRewardConditional(QuestTextConditional questTextConditional) : base(questTextConditional) { }
    }

    [DBTableName("quest_request_items_conditional", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic)]
    public sealed record QuestRequestItemsConditional : QuestTextConditional
    {
        public QuestRequestItemsConditional() { }
        public QuestRequestItemsConditional(QuestTextConditional questTextConditional) : base(questTextConditional) { }
    }
}
