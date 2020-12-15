using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ScalingStatDistribution, HasIndexInData = false)]
    public class ScalingStatDistributionEntry
    {
        public ushort PlayerLevelToItemLevelCurveID { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
    }
}
