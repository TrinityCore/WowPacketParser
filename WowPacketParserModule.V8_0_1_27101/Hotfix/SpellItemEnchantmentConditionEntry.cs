using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellItemEnchantmentCondition, HasIndexInData = false)]
    public class SpellItemEnchantmentConditionEntry
    {
        [HotfixArray(5)]
        public byte[] LtOperandType { get; set; }
        [HotfixArray(5)]
        public uint[] LtOperand { get; set; }
        [HotfixArray(5)]
        public byte[] Operator { get; set; }
        [HotfixArray(5)]
        public byte[] RtOperandType { get; set; }
        [HotfixArray(5)]
        public byte[] RtOperand { get; set; }
        [HotfixArray(5)]
        public byte[] Logic { get; set; }
    }
}
