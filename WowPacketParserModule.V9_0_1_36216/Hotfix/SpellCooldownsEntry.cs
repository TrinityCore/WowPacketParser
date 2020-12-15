using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellCooldowns, HasIndexInData = false)]
    public class SpellCooldownsEntry
    {
        public byte DifficultyID { get; set; }
        public int CategoryRecoveryTime { get; set; }
        public int RecoveryTime { get; set; }
        public int StartRecoveryTime { get; set; }
        public uint SpellID { get; set; }
    }
}
