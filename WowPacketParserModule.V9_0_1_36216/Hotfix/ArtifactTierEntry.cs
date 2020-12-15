using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactTier, HasIndexInData = false)]
    public class ArtifactTierEntry
    {
        public uint ArtifactTier { get; set; }
        public uint MaxNumTraits { get; set; }
        public uint MaxArtifactKnowledge { get; set; }
        public uint KnowledgePlayerCondition { get; set; }
        public uint MinimumEmpowerKnowledge { get; set; }
    }
}
