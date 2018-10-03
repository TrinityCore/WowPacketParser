using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.DurabilityCosts, HasIndexInData = false)]
    public class DurabilityCostsEntry
    {
        [HotfixArray(21)]
        public ushort[] WeaponSubClassCost { get; set; }
        [HotfixArray(8)]
        public ushort[] ArmorSubClassCost { get; set; }
    }
}
