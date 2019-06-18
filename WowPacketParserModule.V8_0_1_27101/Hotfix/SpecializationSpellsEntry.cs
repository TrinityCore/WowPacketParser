using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpecializationSpells)]
    public class SpecializationSpellsEntry
    {
        public string Description { get; set; }
        public uint ID { get; set; }
        public ushort SpecID { get; set; }
        public int SpellID { get; set; }
        public int OverridesSpellID { get; set; }
        public byte DisplayOrder { get; set; }
    }
}
