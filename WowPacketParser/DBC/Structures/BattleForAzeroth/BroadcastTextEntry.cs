using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("BroadcastText")]

    public sealed class BroadcastTextEntry
    {
        public string Text;
        public string Text1;
        [Index(false)]
        public int ID;
        public byte LanguageID;
        public uint ConditionID;
        public ushort EmotesID;
        public byte Flags;
        public uint ChatBubbleDurationMs;
        [Cardinality(2)]
        public uint[] SoundEntriesID = new uint[2];
        [Cardinality(3)]
        public ushort[] EmoteID = new ushort[3];
        [Cardinality(3)]
        public ushort[] EmoteDelay = new ushort[3];
    }
}
