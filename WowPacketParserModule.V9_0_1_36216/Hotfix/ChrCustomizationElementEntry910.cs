using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.ChrCustomizationElement, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class ChrCustomizationElementEntry
    {
        public int ChrCustomizationChoiceID { get; set; }
        public int RelatedChrCustomizationChoiceID { get; set; }
        public int ChrCustomizationGeosetID { get; set; }
        public int ChrCustomizationSkinnedModelID { get; set; }
        public int ChrCustomizationMaterialID { get; set; }
        public int ChrCustomizationBoneSetID { get; set; }
        public int ChrCustomizationCondModelID { get; set; }
        public int ChrCustomizationDisplayInfoID { get; set; }
        public int ChrCustomizationItemGeosetModifyID { get; set; }
    }
}
