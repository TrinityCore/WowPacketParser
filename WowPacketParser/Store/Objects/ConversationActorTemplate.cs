using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    enum ActorType : uint
    {
        WorldObjectActor = 0,
        CreatureActor = 1
    };

    [DBTableName("conversation_actor_template")]
    public sealed class ConversationActorTemplate : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? Id;

        [DBFieldName("CreatureId")]
        public uint? CreatureId;

        [DBFieldName("CreatureModelId")]
        public uint? CreatureModelId;

        public WowGuid Guid;
        public uint? Type;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
