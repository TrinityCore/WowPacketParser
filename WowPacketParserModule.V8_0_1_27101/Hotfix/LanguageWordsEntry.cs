using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LanguageWords, HasIndexInData = false)]
    public class LanguageWordsEntry
    {
        public string Word { get; set; }
        public byte LanguageID { get; set; }
    }
}
