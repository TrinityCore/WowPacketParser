using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.HolidayNames, HasIndexInData = false)]
    public class HolidayNamesEntry
    {
        public string Name { get; set; }
    }
}
