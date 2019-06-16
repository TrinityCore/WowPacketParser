using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SkillLine)]
    public class SkillLineEntry
    {
        public string DisplayName { get; set; }
        public string AlternateVerb { get; set; }
        public string Description { get; set; }
        public string HordeDisplayName { get; set; }
        public string OverrideSourceInfoDisplayName { get; set; }
        public uint ID { get; set; }
        public sbyte CategoryID { get; set; }
        public int SpellIconFileID { get; set; }
        public sbyte CanLink { get; set; }
        public uint ParentSkillLineID { get; set; }
        public int ParentTierIndex { get; set; }
        public ushort Flags { get; set; }
        public int SpellBookSpellID { get; set; }
    }
}
