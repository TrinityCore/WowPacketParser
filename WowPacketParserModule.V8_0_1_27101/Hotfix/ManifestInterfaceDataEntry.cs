using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ManifestInterfaceData, HasIndexInData = false)]
    public class ManifestInterfaceDataEntry
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
