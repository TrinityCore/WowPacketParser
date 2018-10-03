using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ModelFileData)]
    public class ModelFileDataEntry
    {
        public int FileDataID { get; set; }
        public byte Flags { get; set; }
        public byte LodCount { get; set; }
        public uint ModelResourcesID { get; set; }
    }
}
