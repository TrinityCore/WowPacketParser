using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ChatProfanity, HasIndexInData = false)]
    public class ChatProfanityEntry
    {
        public string Text { get; set; }
        public sbyte Language { get; set; }
    }
}
