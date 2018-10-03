using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ModelRibbonQuality, HasIndexInData = false)]
    public class ModelRibbonQualityEntry
    {
        public byte RibbonQualityID { get; set; }
        public int FileDataID { get; set; }
    }
}
