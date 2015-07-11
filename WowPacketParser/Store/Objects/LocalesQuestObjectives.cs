using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_objectives_locale")]
    public sealed class LocalesQuestObjectives
    {
        [DBFieldName("QuestId")]
        public uint QuestId;

        [DBFieldName("StorageIndex")]
        public int StorageIndex;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
