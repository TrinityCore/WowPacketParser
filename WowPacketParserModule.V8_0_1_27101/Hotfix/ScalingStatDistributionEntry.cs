using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ScalingStatDistribution, HasIndexInData = false)]
    public class ScalingStatDistributionEntry
    {
        public ushort PlayerLevelToItemLevelCurveID { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
    }
}
