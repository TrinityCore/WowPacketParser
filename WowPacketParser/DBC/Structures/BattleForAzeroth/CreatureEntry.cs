using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("Creature")]
    public sealed class CreatureEntry
    {
        public string Name;
        public string NameAlt;
        public string Title;
        public string TitleAlt;
        public sbyte Classification;
        public byte CreatureType;
        public ushort CreatureFamily;
        public byte StartAnimState;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] DisplayID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] DisplayProbability;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] AlwaysItem;
    }
}
