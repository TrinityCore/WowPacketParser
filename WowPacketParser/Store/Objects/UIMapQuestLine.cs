using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("ui_map_quest_line", TargetedDatabaseFlag.SinceTheWarWithin)]
    public sealed record UIMapQuestLine : IDataModel
    {
        [DBFieldName("UIMapId", true)]
        public uint? UIMapId;

        [DBFieldName("QuestLineId", true)]
        public uint? QuestLineId;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
