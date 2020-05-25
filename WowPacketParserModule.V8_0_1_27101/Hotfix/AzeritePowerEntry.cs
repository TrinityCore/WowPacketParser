using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeritePower, HasIndexInData = false)]
    public class AzeritePowerEntry
    {
        public int SpellID { get; set; }
        public int ItemBonusListID { get; set; }
        public int SpecSetID { get; set; }
        public int Flags { get; set; }
    }
}
