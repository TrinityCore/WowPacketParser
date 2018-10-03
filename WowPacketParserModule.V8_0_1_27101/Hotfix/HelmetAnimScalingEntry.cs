using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.HelmetAnimScaling, HasIndexInData = false)]
    public class HelmetAnimScalingEntry
    {
        public int RaceID { get; set; }
        public float Amount { get; set; }
        public int HelmetGeosetVisDataID { get; set; }
    }
}
