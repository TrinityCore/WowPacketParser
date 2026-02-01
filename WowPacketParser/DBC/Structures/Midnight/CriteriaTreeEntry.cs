using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
{
    [DBFile("CriteriaTree")]
    public sealed class CriteriaTreeEntry
    {
        [Index(true)]
        public uint ID;
        public string Description;
        public uint Parent;
        public uint Amount;
        public int Operator;
        public uint CriteriaID;
        public int OrderIndex;
        public int Flags;
    }
}
