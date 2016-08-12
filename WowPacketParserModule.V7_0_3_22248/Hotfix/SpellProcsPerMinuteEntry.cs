using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellProcsPerMinute, HasIndexInData = false)]
    public class SpellProcsPerMinuteEntry
    {
        public float BaseProcRate { get; set; }
        public byte Flags { get; set; }
    }
}