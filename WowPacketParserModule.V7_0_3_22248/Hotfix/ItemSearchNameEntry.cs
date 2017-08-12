using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSearchName, HasIndexInData = false)]
    public class ItemSearchNameEntry
    {
        public string Name { get; set; }
        [HotfixArray(3)]
        public uint[] Flags { get; set; }
        public uint AllowableRace { get; set; }
        public uint RequiredSpell { get; set; }
        public ushort RequiredReputationFaction { get; set; }
        public ushort RequiredSkill { get; set; }
        public ushort RequiredSkillRank { get; set; }
        public ushort ItemLevel { get; set; }
        public byte Quality { get; set; }
        public byte RequiredExpansion { get; set; }
        public byte RequiredReputationRank { get; set; }
        public byte RequiredLevel { get; set; }
        public uint AllowableClass { get; set; }
    }
}