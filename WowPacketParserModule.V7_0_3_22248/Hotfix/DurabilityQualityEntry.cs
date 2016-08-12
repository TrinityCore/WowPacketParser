using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.DurabilityQuality, HasIndexInData = false)]
    public class DurabilityQualityEntry
    {
        public float QualityMod { get; set; }
    }
}