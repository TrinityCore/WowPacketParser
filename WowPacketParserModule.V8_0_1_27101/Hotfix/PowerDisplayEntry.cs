using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PowerDisplay, HasIndexInData = false)]
    public class PowerDisplayEntry
    {
        public string GlobalStringBaseTag { get; set; }
        public byte ActualType { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
    }
}
