using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("conversation_actors")]
    public sealed class ConversationActor : IDataModel
    {
        [DBFieldName("ConversationId", true)]
        public uint? ConversationId;

        [DBFieldName("ConversationActorId", true)]
        public uint? ConversationActorId;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        public WowGuid Guid;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
