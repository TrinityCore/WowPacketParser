using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GuildColorBackground, HasIndexInData = false)]
    public class GuildColorBackgroundEntry
    {
        public byte Red { get; set; }
        public byte Blue { get; set; }
        public byte Green { get; set; }
    }
}
