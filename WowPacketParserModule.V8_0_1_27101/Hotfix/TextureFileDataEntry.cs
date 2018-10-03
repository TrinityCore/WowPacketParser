using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TextureFileData)]
    public class TextureFileDataEntry
    {
        public int FileDataID { get; set; }
        public byte UsageType { get; set; }
        public int MaterialResourcesID { get; set; }
    }
}
