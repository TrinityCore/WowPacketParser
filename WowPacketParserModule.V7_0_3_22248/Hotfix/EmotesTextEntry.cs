using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.EmotesText, HasIndexInData = false)]
    public class EmotesTextEntry
    {
        public string Name { get; set; }
        public ushort EmoteID { get; set; }
    }
}