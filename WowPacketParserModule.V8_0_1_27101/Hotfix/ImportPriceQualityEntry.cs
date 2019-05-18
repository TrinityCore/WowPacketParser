using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ImportPriceQuality, HasIndexInData = false)]
    public class ImportPriceQualityEntry
    {
        public float Data { get; set; }
    }
}
