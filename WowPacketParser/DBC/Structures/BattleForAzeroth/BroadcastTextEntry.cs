namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("BroadcastText")]

    public sealed class BroadcastTextEntry
    {
        public string Text;
        public string Text1;
        public int ID;
        public byte LanguageID;
        public uint ConditionID;
        public ushort EmotesID;
        public byte Flags;
        public uint ChatBubbleDurationMs;
        public uint[] SoundEntriesID = new uint[2];
        public ushort[] EmoteID = new ushort[3];
        public ushort[] EmoteDelay = new ushort[3];
    }
}
