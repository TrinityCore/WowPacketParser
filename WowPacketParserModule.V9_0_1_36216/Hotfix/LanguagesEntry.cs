using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.Languages)]
    public class LanguagesEntry
    {
        public uint ID { get; set; }
        public string Name { get; set; }
    }
}
