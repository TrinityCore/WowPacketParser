using System.Runtime.InteropServices;

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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] SoundEntriesID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public ushort[] EmoteID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public ushort[] EmoteDelay;
    }
}
