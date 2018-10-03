using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemAppearanceXUiCamera, HasIndexInData = false)]
    public class ItemAppearanceXUiCameraEntry
    {
        public ushort ItemAppearanceID { get; set; }
        public ushort UiCameraID { get; set; }
    }
}
