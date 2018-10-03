using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GameTips, HasIndexInData = false)]
    public class GameTipsEntry
    {
        public string Text { get; set; }
        public byte SortIndex { get; set; }
        public ushort MinLevel { get; set; }
        public ushort MaxLevel { get; set; }
    }
}
