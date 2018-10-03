using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiWidgetVisualization, HasIndexInData = false)]
    public class UiWidgetVisualizationEntry
    {
        public sbyte VisType { get; set; }
        public int TextureKit { get; set; }
        public int FrameTextureKit { get; set; }
        public short WidgetWidth { get; set; }
    }
}
