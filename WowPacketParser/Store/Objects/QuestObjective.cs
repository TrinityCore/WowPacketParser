using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_objectives")]
    public sealed class QuestObjective : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("QuestID")]
        public uint? QuestID;

        [DBFieldName("Type")]
        public QuestRequirementType? Type;

        [DBFieldName("StorageIndex")]
        public int? StorageIndex;

        [DBFieldName("ObjectID")]
        public int? ObjectID;

        [DBFieldName("Amount")]
        public int? Amount;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("UnkFloat")]
        public float? UnkFloat;

        [DBFieldName("Description", LocaleConstant.enUS)]
        public string Description;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}