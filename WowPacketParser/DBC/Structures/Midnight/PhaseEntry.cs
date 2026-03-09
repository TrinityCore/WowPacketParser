using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
{
    [DBFile("Phase")]
    public sealed class PhaseEntry
    {
        [Index(true)]
        public uint ID;
        public int Flags;
    }
}
