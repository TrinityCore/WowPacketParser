using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogHoliday, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class TransmogHolidayEntry
    {
        public int RequiredTransmogHoliday { get; set; }
    }
}
