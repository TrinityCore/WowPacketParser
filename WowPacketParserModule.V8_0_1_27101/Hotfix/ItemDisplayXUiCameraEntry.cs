using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemDisplayXUiCamera, HasIndexInData = false)]
    public class ItemDisplayXUiCameraEntry
    {
        public int ItemDisplayInfoID { get; set; }
        public ushort UiCameraID { get; set; }
    }
}
