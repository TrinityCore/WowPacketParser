using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ChrCustomizationElement)]
    public class ChrCustomizationElementEntry
    {
        public uint ID { get; set; }
        public int ChrCustomizationChoiceID { get; set; }
        public int RelatedChrCustomizationChoiceID { get; set; }
        public int ChrCustomizationGeosetID { get; set; }
        public int ChrCustomizationSkinnedModelID { get; set; }
        public int ChrCustomizationMaterialID { get; set; }
        public int ChrCustomizationBoneSetID { get; set; }
        public int ChrCustomizationCondModelID { get; set; }
        public int ChrCustomizationDisplayInfoID { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_0_5_37503, false)]
        public int ChrCustomizationItemGeosetModifyID { get; set; }
    }
}
