using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.Holidays)]
    public class HolidaysEntry
    {
        public int ID { get; set; }
        [HotfixArray(10)]
        public int[] Duration { get; set; }
        [HotfixArray(16)]
        public int[] Date { get; set; }
        public int Region { get; set; }
        public int Looping { get; set; }
        [HotfixArray(10)]
        public int[] CalendarFlags { get; set; }
        public int HolidayNameID { get; set; }
        public int HolidayDescriptionID { get; set; }
        public string SourceDescription { get; set; }
        public int Priority { get; set; }
        public int CalendarFilterType { get; set; }
        public int Flags { get; set; }
    }
}
