using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDifficulty, HasIndexInData = false)]
    public class CreatureDifficultyEntry
    {
        public sbyte ExpansionID { get; set; }
        public sbyte MinLevel { get; set; }
        public sbyte MaxLevel { get; set; }
        public ushort FactionID { get; set; }
        public int ContentTuningID { get; set; }
        [HotfixArray(7)]
        public uint[] Flags { get; set; }
        public int CreatureID { get; set; }
    }
}
