using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
{
    [DBFile("ConversationLine")]
    public sealed class ConversationLineEntry
    {
        [Index(true)]
        public uint ID;
        public uint BroadcastTextID;
        public uint Unknown1020;
        public uint SpellVisualKitID;
        public int AdditionalDuration;
        public ushort NextConversationLineID;
        public ushort AnimKitID;
        public byte SpeechType;
        public byte StartAnimation;
        public byte EndAnimation;
    }
}
