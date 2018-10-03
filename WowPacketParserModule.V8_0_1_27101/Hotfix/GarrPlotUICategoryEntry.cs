using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrPlotUICategory, HasIndexInData = false)]
    public class GarrPlotUICategoryEntry
    {
        public string CategoryName { get; set; }
        public byte PlotType { get; set; }
    }
}
