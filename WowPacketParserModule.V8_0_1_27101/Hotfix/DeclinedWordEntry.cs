using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.DeclinedWord)]
    public class DeclinedWordEntry
    {
        public string Word { get; set; }
        public int ID { get; set; }
    }
}
