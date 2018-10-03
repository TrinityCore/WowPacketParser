using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiCamFbackTransmogChrRace, HasIndexInData = false)]
    public class UiCamFbackTransmogChrRaceEntry
    {
        public byte ChrRaceID { get; set; }
        public byte Gender { get; set; }
        public byte InventoryType { get; set; }
        public byte Variation { get; set; }
        public ushort UiCameraID { get; set; }
    }
}
