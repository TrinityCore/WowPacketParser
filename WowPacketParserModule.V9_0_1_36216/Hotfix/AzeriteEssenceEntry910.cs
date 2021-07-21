using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteEssence, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class AzeriteEssenceEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SpecSetID { get; set; }
    }
}
