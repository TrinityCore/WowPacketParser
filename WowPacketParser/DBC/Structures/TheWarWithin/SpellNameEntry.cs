using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("SpellName")]
    public sealed class SpellNameEntry
    {
        [Index(true)]
        public uint ID;
        public string Name;
    }
}
