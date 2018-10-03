using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemDisplayInfoMaterialRes, HasIndexInData = false)]
    public class ItemDisplayInfoMaterialResEntry
    {
        public sbyte ComponentSection { get; set; }
        public int MaterialResourcesID { get; set; }
        public int ItemDisplayInfoID { get; set; }
    }
}
