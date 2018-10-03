using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UIExpansionDisplayInfoIcon, HasIndexInData = false)]
    public class UIExpansionDisplayInfoIconEntry
    {
        public string FeatureDescription { get; set; }
        public int ParentID { get; set; }
        public int FeatureIcon { get; set; }
    }
}
