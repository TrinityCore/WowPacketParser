using DBCD.IO.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
{
    [DBFile("Faction")]
    public sealed class FactionEntry
    {
        [Index(true)]
        public uint ID;
        public string Name;
        public string Description;
        public short ReputationIndex;
        public ushort ParentFactionID;
        public byte Expansion;
        public uint FriendshipRepID;
        public int Flags;
        public ushort ParagonFactionID;
        public int RenownFactionID;
        public int RenownCurrencyID;
        [Cardinality(4)]
        public short[] ReputationClassMask = new short[4];
        [Cardinality(4)]
        public ushort[] ReputationFlags = new ushort[4];
        [Cardinality(4)]
        public int[] ReputationBase = new int[4];
        [Cardinality(4)]
        public int[] ReputationMax = new int[4];
        [Cardinality(2)]
        public float[] ParentFactionMod = new float[2];
        [Cardinality(2)]
        public byte[] ParentFactionCap = new byte[2];
        [Cardinality(2)]
        public int[] ReputationRaceMask1 = new int[2];
        [Cardinality(2)]
        public int[] ReputationRaceMask2 = new int[2];
        [Cardinality(2)]
        public int[] ReputationRaceMask3 = new int[2];
        [Cardinality(2)]
        public int[] ReputationRaceMask4 = new int[2];
    }
}
