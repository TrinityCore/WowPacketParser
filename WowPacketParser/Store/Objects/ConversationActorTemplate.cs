using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    enum ActorType : uint
    {
        WorldObjectActor = 0,
        CreatureActor = 1
    };

    [DBTableName("conversation_actor_template", TargetedDatabase.Zero, TargetedDatabase.BattleForAzeroth)]
    public sealed record ConversationActorTemplate : IDataModel
    {
        [DBFieldName("Id", true)]
        public int? Id;

        [DBFieldName("CreatureId")]
        public uint? CreatureId;

        [DBFieldName("CreatureModelId")]
        public uint? CreatureModelId;

        public WowGuid Guid;
        public uint? Type;
        public bool? NoActorObject;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
