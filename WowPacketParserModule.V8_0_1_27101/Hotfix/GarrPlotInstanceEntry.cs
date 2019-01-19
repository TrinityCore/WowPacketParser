using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrPlotInstance, HasIndexInData = false)]
    public class GarrPlotInstanceEntry
    {
        public string Name { get; set; }
        public byte GarrPlotID { get; set; }
    }
}
