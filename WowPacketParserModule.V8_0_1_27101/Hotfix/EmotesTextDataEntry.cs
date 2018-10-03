using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.EmotesTextData, HasIndexInData = false)]
    public class EmotesTextDataEntry
    {
        public string Text { get; set; }
        public byte RelationshipFlags { get; set; }
        public ushort EmotesTextID { get; set; }
    }
}
