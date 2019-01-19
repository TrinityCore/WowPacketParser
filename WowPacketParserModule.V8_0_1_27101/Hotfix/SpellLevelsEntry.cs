using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellLevels, HasIndexInData = false)]
    public class SpellLevelsEntry
    {
        public byte DifficultyID { get; set; }
        public ushort BaseLevel { get; set; }
        public ushort MaxLevel { get; set; }
        public ushort SpellLevel { get; set; }
        public byte MaxPassiveAuraLevel { get; set; }
        public uint SpellID { get; set; } // Ref
    }
}
