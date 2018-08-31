using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("Creature")]
    public sealed class CreatureEntry
    {
        public string Name;
        public string FemaleName;
        public string SubName;
        public string FemaleSubName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] Item;
        public uint Mount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] DisplayID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] DisplayIdProbability;
        public byte Type;
        public byte Family;
        public byte Classification;
        public byte InhabitType;
    }
}
