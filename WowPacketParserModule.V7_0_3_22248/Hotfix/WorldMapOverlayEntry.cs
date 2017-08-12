using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.WorldMapOverlay)]
    public class WorldMapOverlayEntry
    {
        public uint ID { get; set; }
        public string TextureName { get; set; }
        public ushort TextureWidth { get; set; }
        public ushort TextureHeight { get; set; }
        public uint MapAreaID { get; set; }
        [HotfixArray(4)]
        public uint[] AreaID { get; set; }
        public uint OffsetX { get; set; }
        public uint OffsetY { get; set; }
        public uint HitRectTop { get; set; }
        public uint HitRectLeft { get; set; }
        public uint HitRectBottom { get; set; }
        public uint HitRectRight { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint Flags { get; set; }
    }
}