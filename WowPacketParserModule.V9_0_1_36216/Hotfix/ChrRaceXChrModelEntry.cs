using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ChrRaceXChrModel, HasIndexInData = false)]
    public class ChrRaceXChrModelEntry
    {
        public int ChrRacesID { get; set; }
        public int ChrModelID { get; set; }
    }
}
