using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ImportPriceShield, HasIndexInData = false)]
    public class ImportPriceShieldEntry
    {
        public float Data { get; set; }
    }
}
