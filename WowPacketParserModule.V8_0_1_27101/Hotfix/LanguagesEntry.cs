using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Languages)]
    public class LanguagesEntry
    {
        public string Name { get; set; }
        public uint ID { get; set; }
    }
}
