using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Dragonflight
{
    [DBFile("Phase")]
    public sealed class PhaseEntry
    {
        [Index(true)]
        public uint ID;
        public short Flags;
    }
}
