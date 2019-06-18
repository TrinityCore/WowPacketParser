using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrBuildingPlotInst)]
    public class GarrBuildingPlotInstEntry
    {
        [HotfixArray(2, true)]
        public float[] MapOffset { get; set; }
        public uint ID { get; set; }
        public byte GarrBuildingID { get; set; }
        public ushort GarrSiteLevelPlotInstID { get; set; }
        public ushort UiTextureAtlasMemberID { get; set; }
    }
}
