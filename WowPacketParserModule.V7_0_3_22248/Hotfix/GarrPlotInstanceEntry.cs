using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GarrPlotInstance, HasIndexInData = false)]
    public class GarrPlotInstanceEntry
    {
        public string Name { get; set; }
        public byte GarrPlotID { get; set; }
    }
}