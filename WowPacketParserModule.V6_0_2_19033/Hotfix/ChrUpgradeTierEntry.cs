using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.ChrUpgradeTier)]
    public class ChrUpgradeTierEntry
    {
        public uint ID { get; set; }
        public uint TierIndex { get; set; }
        public string Name { get; set; }
        public uint TalentTier { get; set; }
    }
}