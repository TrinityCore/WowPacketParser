using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("conversation_actors")]
    public sealed class ConversationActor : IDataModel
    {
        [DBFieldName("ConversationId", true)]
        public uint? ConversationId;

        [DBFieldName("ConversationActorId")]
        public int? ConversationActorId;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("CreatureId", TargetedDatabase.Shadowlands)]
        public uint? CreatureId;

        [DBFieldName("CreatureModelId", TargetedDatabase.Shadowlands)]
        public uint? CreatureModelId;

        public WowGuid Guid;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
