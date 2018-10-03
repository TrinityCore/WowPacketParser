using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.FootprintTextures, HasIndexInData = false)]
    public class FootprintTexturesEntry
    {
        public int FileDataID { get; set; }
        public int TextureBlendsetID { get; set; }
        public int Flags { get; set; }
    }
}
