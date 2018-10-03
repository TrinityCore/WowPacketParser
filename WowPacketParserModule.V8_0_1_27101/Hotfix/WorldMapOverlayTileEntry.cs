using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WorldMapOverlayTile, HasIndexInData = false)]
    public class WorldMapOverlayTileEntry
    {
        public byte RowIndex { get; set; }
        public byte ColIndex { get; set; }
        public byte LayerIndex { get; set; }
        public int FileDataID { get; set; }
        public int WorldMapOverlayID { get; set; }
    }
}
