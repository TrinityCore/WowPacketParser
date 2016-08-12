using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ChrSpecialization)]
    public class ChrSpecializationEntry
    {
        [HotfixArray(2)]
        public uint[] MasterySpellID { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Description { get; set; }
        public string BackgroundFile { get; set; }
        public ushort SpellIconID { get; set; }
        public byte ClassID { get; set; }
        public byte OrderIndex { get; set; }
        public byte PetTalentType { get; set; }
        public byte Role { get; set; }
        public byte PrimaryStatOrder { get; set; }
        public uint ID { get; set; }
        public uint Flags { get; set; }
        public uint AnimReplacementSetID { get; set; }
    }
}