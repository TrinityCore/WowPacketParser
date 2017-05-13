using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_text")]
    public sealed class CreatureText : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("groupid", true, true)]
        public string GroupId;

        [DBFieldName("id", true, true)]
        public string ID;

        [DBFieldName("text")]
        public string Text;

        [DBFieldName("type")]
        public ChatMessageType? Type;

        [DBFieldName("language")]
        public Language? Language;

        [DBFieldName("probability")]
        public float? Probability;

        [DBFieldName("emote")]
        public EmoteType? Emote;

        [DBFieldName("duration")]
        public uint? Duration;

        [DBFieldName("sound")]
        public uint? Sound;

        [DBFieldName("BroadcastTextId", false, true)]
        public object BroadcastTextID;

        [DBFieldName("comment")]
        public string Comment;

        public WowGuid SenderGUID;
        public string SenderName;
        public WowGuid ReceiverGUID;
        public string ReceiverName;

        public string BroadcastTextIDHelper;
    }
}
