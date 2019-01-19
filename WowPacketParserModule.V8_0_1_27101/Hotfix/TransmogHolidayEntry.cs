using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogHoliday)]
    public class TransmogHolidayEntry
    {
        public int ID { get; set; }
        public int RequiredTransmogHoliday { get; set; }
    }
}
