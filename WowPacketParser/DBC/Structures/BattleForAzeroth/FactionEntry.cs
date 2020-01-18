namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("Faction")]
    public sealed class FactionEntry
    {
        public long[] ReputationRaceMask = new long[4];
        public string Name;
        public string Description;
        public int ID;
        public short ReputationIndex;
        public ushort ParentFactionID;
        public byte Expansion;
        public uint FriendshipRepID;
        public byte Flags;
        public ushort ParagonFactionID;
        public short[] ReputationClassMask = new short[4];
        public ushort[] ReputationFlags = new ushort[4];
        public int[] ReputationBase = new int[4];
        public int[] ReputationMax = new int[4];
        public float[] ParentFactionMod = new float[2];
        public byte[] ParentFactionCap = new byte[2];
    }
}

