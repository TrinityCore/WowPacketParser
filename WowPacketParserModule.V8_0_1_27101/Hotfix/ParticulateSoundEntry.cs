using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ParticulateSound, HasIndexInData = false)]
    public class ParticulateSoundEntry
    {
        public int ParticulateID { get; set; }
        public int DaySound { get; set; }
        public int NightSound { get; set; }
        public int EnterSound { get; set; }
        public int ExitSound { get; set; }
    }
}
