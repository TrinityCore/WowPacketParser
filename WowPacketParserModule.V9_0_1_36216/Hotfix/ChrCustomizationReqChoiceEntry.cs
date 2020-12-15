using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ChrCustomizationReqChoice, HasIndexInData = false)]
    public class ChrCustomizationReqChoiceEntry
    {
        public int ChrCustomizationChoiceID { get; set; }
        public uint ChrCustomizationReqID { get; set; }
    }
}
