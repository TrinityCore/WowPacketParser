using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSearchName)]
    public class ItemSearchNameEntry
    {
        public ulong AllowableRace { get; set; }
        public string Display { get; set; }
        public int ID { get; set; }
        public byte OverallQualityID { get; set; }
        public byte ExpansionID { get; set; }
        public ushort MinFactionID { get; set; }
        public byte MinReputation { get; set; }
        public int AllowableClass { get; set; }
        public sbyte RequiredLevel { get; set; }
        public ushort RequiredSkill { get; set; }
        public ushort RequiredSkillRank { get; set; }
        public uint RequiredAbility { get; set; }
        public ushort ItemLevel { get; set; }
        [HotfixArray(4)]
        public uint[] Flags { get; set; }
    }
}
