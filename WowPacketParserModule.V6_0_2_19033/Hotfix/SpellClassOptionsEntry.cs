using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.SpellClassOptions)]
    public class SpellClassOptionsEntry
    {
        public uint ID { get; set; }
        public uint ModalNextSpell { get; set; }
        [HotfixArray(4)]
        public uint[] SpellClassMask { get; set; }
        public uint SpellClassSet { get; set; }
    }
}