using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
{
    [DBFile("Faction")]
    public sealed class FactionEntry
    {
        [Cardinality(4)]
        public long[] ReputationRaceMask = new long[4];
        public string Name;
        public string Description;
        [Index(false)]
        public uint ID;
        public short ReputationIndex;
        public ushort ParentFactionID;
        public byte Expansion;
        public byte FriendshipRepID;
        public int Unknown341_0;
        public ushort Unknown341_1;
        public int Unknown341_2;
        public int Unknown341_3;
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
    }
}
