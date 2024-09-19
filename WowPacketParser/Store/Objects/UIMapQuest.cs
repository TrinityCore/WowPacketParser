using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("ui_map_quest", TargetedDatabaseFlag.SinceTheWarWithin)]
    public sealed record UIMapQuest : IDataModel
    {
        [DBFieldName("UIMapId", true)]
        public uint? UIMapId;

        [DBFieldName("QuestId", true)]
        public uint? QuestId;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
