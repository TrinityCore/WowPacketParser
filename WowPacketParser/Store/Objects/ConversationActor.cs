using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("conversation_actors")]
    public sealed record ConversationActor : IDataModel
    {
        [DBFieldName("ConversationId", true)]
        public uint? ConversationId;

        [DBFieldName("ConversationActorId")]
        public int? ConversationActorId;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("CreatureId", TargetedDatabase.Shadowlands)]
        public uint? CreatureId;

        [DBFieldName("CreatureDisplayInfoId", TargetedDatabase.Shadowlands)]
        public uint? CreatureDisplayInfoId;

        [DBFieldName("NoActorObject", TargetedDatabase.Shadowlands)]
        public bool? NoActorObject;

        [DBFieldName("ActivePlayerObject", TargetedDatabase.Shadowlands)]
        public bool? ActivePlayerObject;

        public WowGuid Guid;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
