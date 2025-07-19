using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Shadowlands
{
    [DBFile("ConversationLine")]
    public sealed class ConversationLineEntry
    {
        [Index(true)]
        public uint ID;
        public uint BroadcastTextID;
        public uint SpellVisualKitID;
        public int AdditionalDuration;
        public ushort NextConversationLineID;
        public ushort AnimKitID;
        public byte SpeechType;
        public byte StartAnimation;
        public byte EndAnimation;
    }
}
