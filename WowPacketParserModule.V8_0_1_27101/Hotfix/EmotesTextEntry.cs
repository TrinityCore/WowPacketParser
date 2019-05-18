using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.EmotesText, HasIndexInData = false)]
    public class EmotesTextEntry
    {
        public string Name { get; set; }
        public ushort EmoteID { get; set; }
    }
}
