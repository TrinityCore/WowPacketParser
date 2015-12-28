
using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFileName("Faction")]
    public sealed class FactionEntry
    {
        public uint[] ReputationRaceMask;
        public int[] ReputationBase;
        public float[] ParentFactionMod;
        public string Name;
        public string Description;
        public uint[] ReputationMax;
        public short ReputationIndex;
        public ushort[] ReputationClassMask;
        public ushort[] ReputationFlags;
        public ushort ParentFactionID;
        public byte[] ParentFactionCap;
        public byte Expansion;
        public byte Flags;
        public byte FriendshipRepID;
    }
}

