using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_objectives")]
    public sealed record QuestObjective : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("QuestID")]
        public uint? QuestID;

        [DBFieldName("Type")]
        public QuestRequirementType? Type;

        [DBFieldName("Order", TargetedDatabase.Legion)]
        public uint? Order;

        [DBFieldName("StorageIndex")]
        public int? StorageIndex;

        [DBFieldName("ObjectID")]
        public int? ObjectID;

        [DBFieldName("Amount")]
        public int? Amount;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("Flags2", TargetedDatabase.Legion)] // 7.1.0
        public uint? Flags2;

        [DBFieldName("ProgressBarWeight")]
        public float? ProgressBarWeight;

        [DBFieldName("Description", LocaleConstant.enUS)]
        public string Description;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}