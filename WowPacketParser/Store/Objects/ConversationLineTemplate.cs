using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("conversation_line_template")]
    public sealed record ConversationLineTemplate : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? Id;

        [DBFieldName("StartTime", TargetedDatabaseFlag.TillBattleForAzeroth)]
        public uint? StartTime;

        [DBFieldName("UiCameraID")]
        public uint? UiCameraID;

        [DBFieldName("ActorIdx")]
        public byte? ActorIdx;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ChatType", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
        public byte? ChatType;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
