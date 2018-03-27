using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("BroadcastText")]

    public sealed class BroadcastTextEntry
    {
        public string Text;
        public string Text1;
        public ushort[] EmoteID;
        public ushort[] EmoteDelay;
        public ushort EmotesID;
        public byte LanguageID;
        public byte Flags;
        public uint[] SoundEntriesID;
        public uint ConditionID;
    }
}
