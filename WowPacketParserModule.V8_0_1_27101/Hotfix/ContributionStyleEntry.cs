using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ContributionStyle, HasIndexInData = false)]
    public class ContributionStyleEntry
    {
        public string StateName { get; set; }
        public string TooltipLine { get; set; }
        public int StateColor { get; set; }
        public uint Flags { get; set; }
        public int StatusBarAtlas { get; set; }
        public int BorderAtlas { get; set; }
        public int BannerAtlas { get; set; }
    }
}
