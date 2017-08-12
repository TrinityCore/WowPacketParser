using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GuildColorBackground, HasIndexInData = false)]
    public class GuildColorBackgroundEntry
    {
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
    }
}