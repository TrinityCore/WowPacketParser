using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Holidays, ClientVersionBuild.V7_0_3_22248, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class HolidaysEntry
    {
        [HotfixArray(16)]
        public uint[] Date { get; set; }
        public string TextureFilename { get; set; }
        [HotfixArray(10)]
        public ushort[] Duration { get; set; }
        public ushort Region { get; set; }
        public byte Looping { get; set; }
        [HotfixArray(10)]
        public byte[] CalendarFlags { get; set; }
        public byte HolidayNameID { get; set; }
        public byte HolidayDescriptionID { get; set; }
        public byte Priority { get; set; }
        public sbyte CalendarFilterType { get; set; }
        public byte Flags { get; set; }
    }
}