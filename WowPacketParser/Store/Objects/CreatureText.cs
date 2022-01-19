using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_text")]
    public sealed record CreatureText : IDataModel
    {
        [DBFieldName("CreatureID", true)]
        public uint? Entry;

        [DBFieldName("GroupID", true, true)]
        public string GroupId;

        [DBFieldName("ID", true, true)]
        public string ID;

        [DBFieldName("Text")]
        public string Text;

        [DBFieldName("Type")]
        public ChatMessageType? Type;

        [DBFieldName("Language")]
        public Language? Language;

        [DBFieldName("Probability")]
        public float? Probability;

        [DBFieldName("Emote")]
        public EmoteType? Emote;

        [DBFieldName("Duration")]
        public uint? Duration;

        [DBFieldName("Sound")]
        public uint? Sound;

        [DBFieldName("BroadcastTextId", false, true)]
        public object BroadcastTextID;

        [DBFieldName("TextRange")]
        public byte? TextRange = 0;

        [DBFieldName("comment")]
        public string Comment;

        public WowGuid SenderGUID;
        public string SenderName;
        public WowGuid ReceiverGUID;
        public string ReceiverName;

        public string BroadcastTextIDHelper;
    }
}
