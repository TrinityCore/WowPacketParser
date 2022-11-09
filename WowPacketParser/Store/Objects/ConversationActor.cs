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

        [DBFieldName("CreatureId", TargetedDatabaseFlag.SinceShadowlands)]
        public uint? CreatureId;

        [DBFieldName("CreatureDisplayInfoId", TargetedDatabaseFlag.SinceShadowlands)]
        public uint? CreatureDisplayInfoId;

        [DBFieldName("NoActorObject", TargetedDatabaseFlag.SinceShadowlands)]
        public bool? NoActorObject;

        [DBFieldName("ActivePlayerObject", TargetedDatabaseFlag.SinceShadowlands)]
        public bool? ActivePlayerObject;

        public WowGuid Guid;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
