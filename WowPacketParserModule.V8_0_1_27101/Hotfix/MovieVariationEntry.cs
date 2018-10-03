using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MovieVariation, HasIndexInData = false)]
    public class MovieVariationEntry
    {
        public uint FileDataID { get; set; }
        public uint OverlayFileDataID { get; set; }
        public ushort MovieID { get; set; }
    }
}
