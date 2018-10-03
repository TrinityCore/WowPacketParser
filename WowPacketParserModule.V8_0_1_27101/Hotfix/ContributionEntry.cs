using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Contribution)]
    public class ContributionEntry
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public int ManagedWorldStateInputID { get; set; }
        public int OrderIndex { get; set; }
        public int ContributionStyleContainer { get; set; }
        [HotfixArray(4)]
        public int[] UiTextureAtlasMemberID { get; set; }
    }
}
