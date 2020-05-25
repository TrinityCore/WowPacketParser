using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_3_0_33062.Hotfix
{
    [HotfixStructure(DB2Hash.ItemAppearance, ClientVersionBuild.V8_3_0_33062, HasIndexInData = false)]
    public class ItemAppearanceEntry
    {
        public byte DisplayType { get; set; }
        public int ItemDisplayInfoID { get; set; }
        public int DefaultIconFileDataID { get; set; }
        public int UiOrder { get; set; }
    }
}
