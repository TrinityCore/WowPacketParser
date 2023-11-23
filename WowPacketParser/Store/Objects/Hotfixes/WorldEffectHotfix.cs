using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("world_effect")]
    public sealed record WorldEffectHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("QuestFeedbackEffectID")]
        public uint? QuestFeedbackEffectID;

        [DBFieldName("WhenToDisplay")]
        public byte? WhenToDisplay;

        [DBFieldName("TargetType")]
        public byte? TargetType;

        [DBFieldName("TargetAsset")]
        public int? TargetAsset;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("CombatConditionID")]
        public ushort? CombatConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("world_effect")]
    public sealed record WorldEffectHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("QuestFeedbackEffectID")]
        public uint? QuestFeedbackEffectID;

        [DBFieldName("WhenToDisplay")]
        public byte? WhenToDisplay;

        [DBFieldName("TargetType")]
        public byte? TargetType;

        [DBFieldName("TargetAsset")]
        public int? TargetAsset;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("CombatConditionID")]
        public ushort? CombatConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
