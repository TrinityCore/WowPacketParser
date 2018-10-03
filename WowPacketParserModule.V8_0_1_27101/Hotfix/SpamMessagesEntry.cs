using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpamMessages, HasIndexInData = false)]
    public class SpamMessagesEntry
    {
        public string Text { get; set; }
    }
}
