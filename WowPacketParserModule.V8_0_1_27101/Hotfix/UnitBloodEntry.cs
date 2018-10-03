using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UnitBlood, HasIndexInData = false)]
    public class UnitBloodEntry
    {
        public uint PlayerCritBloodSpurtID { get; set; }
        public uint PlayerHitBloodSpurtID { get; set; }
        public uint DefaultBloodSpurtID { get; set; }
        public uint PlayerOmniCritBloodSpurtID { get; set; }
        public uint PlayerOmniHitBloodSpurtID { get; set; }
        public uint DefaultOmniBloodSpurtID { get; set; }
    }
}
