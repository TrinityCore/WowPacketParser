using PacketParser.Enums;
using PacketParser.SQL;
using PacketParser.Misc;

namespace PacketParser.DataStructures
{
    [DBTableName("creature_text")]
    public sealed class CreatureText : ITextOutputDisabled
    {
        [DBFieldName("groupid")]
        public uint GroupId;

        [DBFieldName("id")]
        public uint Id;

        [DBFieldName("text")]
        public string Text;

        [DBFieldName("type")]
        public ChatMessageType Type;

        [DBFieldName("language")]
        public Language Language;

        [DBFieldName("probability")]
        public float Probability;

        [DBFieldName("emote")]
        public EmoteType Emote;

        [DBFieldName("duration")]
        public uint Duration;

        [DBFieldName("sound")]
        public uint Sound;

        [DBFieldName("comment")]
        public string Comment;
    }
}
