using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellClassOptions, HasIndexInData = false)]
    public class SpellClassOptionsEntry
    {
        public uint SpellID { get; set; }
        [HotfixArray(4)]
        public int[] SpellClassMask { get; set; }
        public byte SpellClassSet { get; set; }
        public uint ModalNextSpell { get; set; }
    }
}