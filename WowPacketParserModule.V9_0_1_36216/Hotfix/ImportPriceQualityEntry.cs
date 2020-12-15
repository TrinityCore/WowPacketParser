using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ImportPriceQuality, HasIndexInData = false)]
    public class ImportPriceQualityEntry
    {
        public float Data { get; set; }
    }
}
