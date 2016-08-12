using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellItemEnchantmentCondition, HasIndexInData = false)]
    public class SpellItemEnchantmentConditionEntry
    {
        [HotfixArray(5)]
        public byte[] LTOperandType { get; set; }
        [HotfixArray(5)]
        public byte[] Operator { get; set; }
        [HotfixArray(5)]
        public byte[] RTOperandType { get; set; }
        [HotfixArray(5)]
        public byte[] RTOperand { get; set; }
        [HotfixArray(5)]
        public byte[] Logic { get; set; }
        [HotfixArray(5)]
        public uint[] LTOperand { get; set; }
    }
}