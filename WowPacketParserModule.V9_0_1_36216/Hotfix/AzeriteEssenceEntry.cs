using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteEssence, ClientVersionBuild.V9_0_1_36216, ClientVersionBuild.V9_1_0_39185)]
    public class AzeriteEssenceEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint ID { get; set; }
        public int SpecSetID { get; set; }
    }
}
