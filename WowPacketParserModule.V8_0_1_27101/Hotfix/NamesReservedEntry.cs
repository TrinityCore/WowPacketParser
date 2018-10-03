using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.NamesReserved, HasIndexInData = false)]
    public class NamesReservedEntry
    {
        public string Name { get; set; }
    }
}
