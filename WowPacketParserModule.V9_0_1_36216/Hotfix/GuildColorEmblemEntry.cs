using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.GuildColorEmblem, HasIndexInData = false)]
    public class GuildColorEmblemEntry
    {
        public byte Red { get; set; }
        public byte Blue { get; set; }
        public byte Green { get; set; }
    }
}
