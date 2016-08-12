using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.NamesReserved, HasIndexInData = false)]
    public class NamesReservedEntry
    {
        public string Name { get; set; }
    }
}