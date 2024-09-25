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

        [DBFieldName("CreatureId", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
        public uint? CreatureId;

        [DBFieldName("CreatureDisplayInfoId", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
        public uint? CreatureDisplayInfoId;

        [DBFieldName("NoActorObject", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
        public bool? NoActorObject;

        [DBFieldName("ActivePlayerObject", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
        public bool? ActivePlayerObject;

        public WowGuid Guid;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
