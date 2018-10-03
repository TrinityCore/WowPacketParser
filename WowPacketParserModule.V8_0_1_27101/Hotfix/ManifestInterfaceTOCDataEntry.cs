using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ManifestInterfaceTOCData, HasIndexInData = false)]
    public class ManifestInterfaceTOCDataEntry
    {
        public string FilePath { get; set; }
    }
}
