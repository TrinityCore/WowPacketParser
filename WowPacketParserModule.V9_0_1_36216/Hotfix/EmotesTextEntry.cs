using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.EmotesText, HasIndexInData = false)]
    public class EmotesTextEntry
    {
        public string Name { get; set; }
        public ushort EmoteID { get; set; }
    }
}
