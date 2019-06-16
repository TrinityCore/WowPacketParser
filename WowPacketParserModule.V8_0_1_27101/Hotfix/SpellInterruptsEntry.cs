using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellInterrupts, HasIndexInData = false)]
    public class SpellInterruptsEntry
    {
        public byte DifficultyID { get; set; }
        public short InterruptFlags { get; set; }
        [HotfixArray(2)]
        public int[] AuraInterruptFlags { get; set; }
        [HotfixArray(2)]
        public int[] ChannelInterruptFlags { get; set; }
        public int SpellID { get; set; }
    }
}
