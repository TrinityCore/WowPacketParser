using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemAppearance, HasIndexInData = false)]
    public class ItemAppearanceEntry
    {
        public byte DisplayType { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_2_5_31921, false)]
        public int SubclassID { get; set; }
        public int ItemDisplayInfoID { get; set; }
        public int DefaultIconFileDataID { get; set; }
        public int UiOrder { get; set; }
    }
}
