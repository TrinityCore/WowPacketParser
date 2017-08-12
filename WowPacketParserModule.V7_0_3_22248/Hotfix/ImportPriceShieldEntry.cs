using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ImportPriceShield, HasIndexInData = false)]
    public class ImportPriceShieldEntry
    {
        public float Factor { get; set; }
    }
}