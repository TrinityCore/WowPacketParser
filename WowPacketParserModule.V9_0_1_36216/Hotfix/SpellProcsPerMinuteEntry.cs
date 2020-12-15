using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellProcsPerMinute, HasIndexInData = false)]
    public class SpellProcsPerMinuteEntry
    {
        public float BaseProcRate { get; set; }
        public byte Flags { get; set; }
    }
}
