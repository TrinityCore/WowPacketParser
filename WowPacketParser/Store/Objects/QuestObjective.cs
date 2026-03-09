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

        [DBFieldName("Order", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.AnyClassic)]
        public uint? Order;

        [DBFieldName("StorageIndex")]
        public int? StorageIndex;

        [DBFieldName("ObjectID")]
        public int? ObjectID;

        [DBFieldName("Amount")]
        public int? Amount;

        [DBFieldName("SecondaryAmount", TargetedDatabaseFlag.TheWarWithin)]
        [DBFieldName("ConditionalAmount", TargetedDatabaseFlag.SinceMidnight)]
        public int? ConditionalAmount;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("Flags2", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.AnyClassic)] // 7.1.0
        public uint? Flags2;

        [DBFieldName("ProgressBarWeight")]
        public float? ProgressBarWeight;

        [DBFieldName("ParentObjectiveID")]
        public int? ParentObjectiveID;

        [DBFieldName("Description", LocaleConstant.enUS)]
        public string Description;

        [DBFieldName("Visible", TargetedDatabaseFlag.SinceTheWarWithin)]
        public bool? Visible;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}