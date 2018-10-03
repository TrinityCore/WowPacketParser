using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiCamFbackTransmogWeapon, HasIndexInData = false)]
    public class UiCamFbackTransmogWeaponEntry
    {
        public byte ItemClass { get; set; }
        public byte ItemSubclass { get; set; }
        public byte InventoryType { get; set; }
        public ushort UiCameraID { get; set; }
    }
}
