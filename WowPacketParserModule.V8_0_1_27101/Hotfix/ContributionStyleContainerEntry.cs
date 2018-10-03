using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ContributionStyleContainer, HasIndexInData = false)]
    public class ContributionStyleContainerEntry
    {
        [HotfixArray(5)]
        public int[] ContributionStyleID { get; set; }
    }
}
