using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactCategory, HasIndexInData = false)]
    public class ArtifactCategoryEntry
    {
        public short XpMultCurrencyID { get; set; }
        public short XpMultCurveID { get; set; }
    }
}
