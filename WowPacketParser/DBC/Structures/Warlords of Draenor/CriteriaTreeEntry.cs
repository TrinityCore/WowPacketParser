namespace WowPacketParser.DBC.Structures
{
    public sealed class CriteriaTreeEntry
    {
        public uint ID;
        public uint CriteriaID;
        public ulong Amount;
        public uint Operator;
        public uint Parent;
        public uint Flags;
        public string Description;
        public uint OrderIndex;

        public uint HACK;
    }
}
