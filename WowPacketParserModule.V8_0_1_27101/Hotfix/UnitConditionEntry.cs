using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UnitCondition, HasIndexInData = false)]
    public class UnitConditionEntry
    {
        public byte Flags { get; set; }
        [HotfixArray(8)]
        public byte[] Variable { get; set; }
        [HotfixArray(8)]
        public sbyte[] Op { get; set; }
        [HotfixArray(8)]
        public int[] Value { get; set; }
    }
}
