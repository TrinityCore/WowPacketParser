using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ComponentTextureFileData, HasIndexInData = false)]
    public class ComponentTextureFileDataEntry
    {
        public byte GenderIndex { get; set; }
        public byte ClassID { get; set; }
        public byte RaceID { get; set; }
    }
}
