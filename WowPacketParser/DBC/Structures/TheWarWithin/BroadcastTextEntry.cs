using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("BroadcastText")]
    public sealed class BroadcastTextEntry
    {
        public string Text;
        public string Text1;
        [Index(false)]
        public uint ID;
        public int LanguageID;
        public int ConditionID;
        public ushort EmotesID;
        public ushort Flags;
        public uint ChatBubbleDurationMs;
        public int VoiceOverPriorityID;
        [Cardinality(2)]
        public uint[] SoundKitID = new uint[2];
        [Cardinality(3)]
        public ushort[] EmoteID = new ushort[3];
        [Cardinality(3)]
        public ushort[] EmoteDelay = new ushort[3];
    }
}
