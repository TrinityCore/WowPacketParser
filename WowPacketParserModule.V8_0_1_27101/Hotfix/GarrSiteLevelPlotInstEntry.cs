using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrSiteLevelPlotInst, HasIndexInData = false)]
    public class GarrSiteLevelPlotInstEntry
    {
        [HotfixArray(2)]
        public float[] UiMarkerPos { get; set; }
        public ushort GarrSiteLevelID { get; set; }
        public byte GarrPlotInstanceID { get; set; }
        public byte UiMarkerSize { get; set; }
    }
}
