using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GarrSiteLevelPlotInst, HasIndexInData = false)]
    public class GarrSiteLevelPlotInstEntry
    {
        [HotfixArray(2)]
        public float[] Landmark { get; set; }
        public ushort GarrSiteLevelID { get; set; }
        public byte GarrPlotInstanceID { get; set; }
        public byte Unknown { get; set; }
    }
}