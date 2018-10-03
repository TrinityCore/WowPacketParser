using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UIExpansionDisplayInfo, HasIndexInData = false)]
    public class UIExpansionDisplayInfoEntry
    {
        public int ExpansionLogo { get; set; }
        public int ExpansionBanner { get; set; }
        public uint ExpansionLevel { get; set; }
    }
}
