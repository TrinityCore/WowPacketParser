using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDifficulty, HasIndexInData = false)]
    public class CreatureDifficultyEntry
    {
        public uint CreatureID { get; set; }
        [HotfixArray(7)]
        public uint[] Flags { get; set; }
        public ushort FactionTemplateID { get; set; }
        public sbyte Expansion { get; set; }
        public sbyte MinLevel { get; set; }
        public sbyte MaxLevel { get; set; }
    }
}
