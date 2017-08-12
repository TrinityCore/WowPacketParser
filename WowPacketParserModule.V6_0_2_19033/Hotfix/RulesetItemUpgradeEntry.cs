using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.RulesetItemUpgrade)]
    public class RulesetItemUpgradeEntry
    {
        public uint ID { get; set; }
        public uint ItemUpgradeLevel { get; set; }
        public uint ItemUpgradeID { get; set; }
        public uint ItemID { get; set; }
    }
}
