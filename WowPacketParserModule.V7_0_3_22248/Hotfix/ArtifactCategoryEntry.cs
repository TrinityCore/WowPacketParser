using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactCategory, HasIndexInData = false)]
    public class ArtifactCategoryEntry
    {
        public ushort ArtifactKnowledgeCurrencyID { get; set; }
        public ushort ArtifactKnowledgeMultiplierCurveID { get; set; }
    }
}
