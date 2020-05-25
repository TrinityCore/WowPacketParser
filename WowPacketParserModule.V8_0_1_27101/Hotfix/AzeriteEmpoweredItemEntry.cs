using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteEmpoweredItem, HasIndexInData = false)]
    public class AzeriteEmpoweredItemEntry
    {
        public int ItemID { get; set; }
        public uint AzeriteTierUnlockSetID { get; set; }
        public uint AzeritePowerSetID { get; set; }
    }
}
