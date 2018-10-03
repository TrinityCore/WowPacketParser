using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrFollowerSetXFollower, HasIndexInData = false)]
    public class GarrFollowerSetXFollowerEntry
    {
        public int GarrFollowerID { get; set; }
        public int GarrFollowerSetID { get; set; }
    }
}
