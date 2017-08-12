using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ImportPriceQuality, HasIndexInData = false)]
    public class ImportPriceQualityEntry
    {
        public float Factor { get; set; }
    }
}