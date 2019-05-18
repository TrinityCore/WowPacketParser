using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellClassOptions, HasIndexInData = false)]
    public class SpellClassOptionsEntry
    {
        public int SpellID { get; set; }
        public uint ModalNextSpell { get; set; }
        public byte SpellClassSet { get; set; }
        [HotfixArray(4)]
        public uint[] SpellClassMask { get; set; }
    }
}
