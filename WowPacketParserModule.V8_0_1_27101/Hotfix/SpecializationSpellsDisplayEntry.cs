using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpecializationSpellsDisplay, HasIndexInData = false)]
    public class SpecializationSpellsDisplayEntry
    {
        public ushort SpecializationID { get; set; }
        [HotfixArray(6)]
        public uint[] SpellID { get; set; }
    }
}
