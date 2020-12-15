using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogHoliday)]
    public class TransmogHolidayEntry
    {
        public uint ID { get; set; }
        public int RequiredTransmogHoliday { get; set; }
    }
}
