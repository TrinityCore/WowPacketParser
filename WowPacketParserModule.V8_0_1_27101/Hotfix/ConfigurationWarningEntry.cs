using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ConfigurationWarning, HasIndexInData = false)]
    public class ConfigurationWarningEntry
    {
        public string Warning { get; set; }
        public uint Type { get; set; }
    }
}
