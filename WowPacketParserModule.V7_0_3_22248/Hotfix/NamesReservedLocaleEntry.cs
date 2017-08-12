using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.NamesReservedLocale, HasIndexInData = false)]
    public class NamesReservedLocaleEntry
    {
        public string Name { get; set; }
        public byte LocaleMask { get; set; }
    }
}