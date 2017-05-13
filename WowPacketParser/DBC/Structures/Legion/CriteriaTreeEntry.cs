using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("CriteriaTree")]
    public sealed class CriteriaTreeEntry
    {
        public uint Amount;
        public string Description;
        public ushort Parent;
        public ushort Flags;
        public byte Operator;
        public uint CriteriaID;
        public int OrderIndex;
    }
}
