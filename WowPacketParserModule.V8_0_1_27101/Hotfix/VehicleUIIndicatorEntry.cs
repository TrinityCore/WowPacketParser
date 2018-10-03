using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.VehicleUIIndicator, HasIndexInData = false)]
    public class VehicleUIIndicatorEntry
    {
        public int BackgroundTextureFileID { get; set; }
    }
}
