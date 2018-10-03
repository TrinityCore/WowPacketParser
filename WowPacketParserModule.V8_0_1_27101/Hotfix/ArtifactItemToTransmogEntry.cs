using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactItemToTransmog, HasIndexInData = false)]
    public class ArtifactItemToTransmogEntry
    {
        public int ChildItemID { get; set; }
        public int ItemID { get; set; }
        public int ArtifactID { get; set; }
    }
}
