using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiMapArtStyleLayer, HasIndexInData = false)]
    public class UiMapArtStyleLayerEntry
    {
        public byte LayerIndex { get; set; }
        public ushort LayerWidth { get; set; }
        public ushort LayerHeight { get; set; }
        public ushort TileWidth { get; set; }
        public ushort TileHeight { get; set; }
        public float MinScale { get; set; }
        public float MaxScale { get; set; }
        public int AdditionalZoomSteps { get; set; }
        public int UiMapArtStyleID { get; set; }
    }
}
