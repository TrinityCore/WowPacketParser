using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.DurabilityQuality, HasIndexInData = false)]
    public class DurabilityQualityEntry
    {
        public float Data { get; set; }
    }
}
