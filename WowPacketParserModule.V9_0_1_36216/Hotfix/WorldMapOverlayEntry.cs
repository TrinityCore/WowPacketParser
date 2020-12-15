using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.WorldMapOverlay)]
    public class WorldMapOverlayEntry
    {
        public uint ID { get; set; }
        public uint UiMapArtID { get; set; }
        public ushort TextureWidth { get; set; }
        public ushort TextureHeight { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public int HitRectTop { get; set; }
        public int HitRectBottom { get; set; }
        public int HitRectLeft { get; set; }
        public int HitRectRight { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint Flags { get; set; }
        [HotfixArray(4)]
        public uint[] AreaID { get; set; }
    }
}
