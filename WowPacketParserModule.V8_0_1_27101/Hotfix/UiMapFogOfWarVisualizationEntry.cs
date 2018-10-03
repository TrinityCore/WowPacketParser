using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiMapFogOfWarVisualization, HasIndexInData = false)]
    public class UiMapFogOfWarVisualizationEntry
    {
        public uint BackgroundAtlasID { get; set; }
        public uint MaskAtlasID { get; set; }
        public float MaskScalar { get; set; }
    }
}
