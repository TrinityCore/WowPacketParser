using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("BroadcastText")]

    public sealed class BroadcastTextEntry
    {
        public string MaleText;
        public string FemaleText;
        public ushort[] EmoteID;
        public ushort[] EmoteDelay;
        public ushort UnkEmoteID;
        public byte Language;
        public byte Type;
        public uint[] SoundID;
        public uint PlayerConditionID;
    }
}
