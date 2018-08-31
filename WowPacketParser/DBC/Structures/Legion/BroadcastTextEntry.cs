using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("BroadcastText")]

    public sealed class BroadcastTextEntry
    {
        public string Text;
        public string Text1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public ushort[] EmoteID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public ushort[] EmoteDelay;
        public ushort EmotesID;
        public byte LanguageID;
        public byte Flags;
        public uint ConditionID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] SoundEntriesID;
    }
}
