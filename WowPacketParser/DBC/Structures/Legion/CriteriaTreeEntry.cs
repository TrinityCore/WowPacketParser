namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("CriteriaTree")]
    public sealed class CriteriaTreeEntry
    {
        public string Description;
        public int Amount;
        public short Flags;
        public byte Operator;
        public uint CriteriaID;
        public uint Parent;
        public int OrderIndex;
    }
}
