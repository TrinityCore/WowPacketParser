using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ChrSpecialization)]
    public class ChrSpecializationEntry
    {
        public string Name { get; set; }
        public string FemaleName { get; set; }
        public string Description { get; set; }
        public uint ID { get; set; }
        public sbyte ClassID { get; set; }
        public sbyte OrderIndex { get; set; }
        public sbyte PetTalentType { get; set; }
        public sbyte Role { get; set; }
        public uint Flags { get; set; }
        public int SpellIconFileID { get; set; }
        public sbyte PrimaryStatPriority { get; set; }
        public int AnimReplacements { get; set; }
        [HotfixArray(2)]
        public int[] MasterySpellID { get; set; }
    }
}
