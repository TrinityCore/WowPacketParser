using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.HolidayDescriptions, HasIndexInData = false)]
    public class HolidayDescriptionsEntry
    {
        public string Description { get; set; }
    }
}
