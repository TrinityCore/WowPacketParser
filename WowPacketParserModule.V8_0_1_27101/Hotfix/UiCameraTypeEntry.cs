using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiCameraType, HasIndexInData = false)]
    public class UiCameraTypeEntry
    {
        public string Name { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
    }
}
