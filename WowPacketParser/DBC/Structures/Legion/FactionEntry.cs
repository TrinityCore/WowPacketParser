using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("Faction")]
    public sealed class FactionEntry
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] ReputationRaceMask;
        public string Name;
        public string Description;
        public uint ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] ReputationBase;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public float[] ParentFactionMod;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] ReputationMax;
        public short ReputationIndex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] ReputationClassMask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] ReputationFlags;
        public ushort ParentFactionID;
        public ushort ParagonFactionID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] ParentFactionCap;
        public byte Expansion;
        public byte Flags;
        public byte FriendshipRepID;
    }
}

