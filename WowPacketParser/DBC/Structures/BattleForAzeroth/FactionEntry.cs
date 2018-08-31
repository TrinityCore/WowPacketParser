using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("Faction")]
    public sealed class FactionEntry
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public long[] ReputationRaceMask;
        public string Name;
        public string Description;
        public int ID;
        public short ReputationIndex;
        public ushort ParentFactionID;
        public byte Expansion;
        public byte FriendshipRepID;
        public byte Flags;
        public ushort ParagonFactionID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public short[] ReputationClassMask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] ReputationFlags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] ReputationBase;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] ReputationMax;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public float[] ParentFactionMod;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] ParentFactionCap;
    }
}

