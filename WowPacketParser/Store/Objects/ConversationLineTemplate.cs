using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("conversation_line_template")]
    public sealed class ConversationLineTemplate : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? Id;

        [DBFieldName("StartTime")]
        public uint? StartTime;

        [DBFieldName("UiCameraID")]
        public uint? UiCameraID;

        [DBFieldName("ActorIdx")]
        public ushort? ActorIdx;

        [DBFieldName("Unk")]
        public ushort? Unk;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
