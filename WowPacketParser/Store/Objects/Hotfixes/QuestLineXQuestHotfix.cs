using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("quest_line_x_quest")]
    public sealed record QuestLineXQuestHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("QuestLineID")]
        public uint? QuestLineID;

        [DBFieldName("QuestID")]
        public uint? QuestID;

        [DBFieldName("OrderIndex")]
        public uint? OrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("quest_line_x_quest")]
    public sealed record QuestLineXQuestHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("QuestLineID")]
        public uint? QuestLineID;

        [DBFieldName("QuestID")]
        public uint? QuestID;

        [DBFieldName("OrderIndex")]
        public uint? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
