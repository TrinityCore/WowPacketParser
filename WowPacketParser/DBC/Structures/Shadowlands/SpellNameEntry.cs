using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Shadowlands
{
    [DBFile("SpellName")]
    public sealed class SpellNameEntry
    {
        [Index(true)]
        public uint ID;
        public string Name;
    }
}
