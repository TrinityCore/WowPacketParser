using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.QuestInfo, HasIndexInData = false)]
    public class QuestInfoEntry
    {
        public string InfoName { get; set; }
        public sbyte Type { get; set; }
        public byte Modifiers { get; set; }
        public ushort Profession { get; set; }
    }
}
