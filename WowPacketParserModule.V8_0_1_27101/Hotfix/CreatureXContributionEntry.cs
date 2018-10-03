using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureXContribution)]
    public class CreatureXContributionEntry
    {
        public int ID { get; set; }
        public int ContributionID { get; set; }
        public int CreatureID { get; set; }
    }
}
