using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
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
        public uint SpellID { get; set; }
    }
}
