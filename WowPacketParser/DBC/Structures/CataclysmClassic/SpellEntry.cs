using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
{
    [DBFile("Spell")]
    public sealed class SpellEntry
    {
        [Index(true)]
        public uint ID;
        public string NameSubtext;
        public string Description;
        public string AuraDescription;
    }
}
