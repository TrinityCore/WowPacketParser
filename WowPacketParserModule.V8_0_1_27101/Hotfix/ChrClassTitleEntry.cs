using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ChrClassTitle, HasIndexInData = false)]
    public class ChrClassTitleEntry
    {
        public string NameMale { get; set; }
        public string NameFemale { get; set; }
        public byte ChrClassID { get; set; }
    }
}
