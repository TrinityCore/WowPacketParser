using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpecializationSpells)]
    public class SpecializationSpellsEntry
    {
        public uint SpellID { get; set; }
        public uint OverridesSpellID { get; set; }
        public string Description { get; set; }
        public ushort SpecID { get; set; }
        public byte OrderIndex { get; set; }
        public uint ID { get; set; }
    }
}