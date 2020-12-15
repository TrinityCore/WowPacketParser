using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSet, HasIndexInData = false)]
    public class ItemSetEntry
    {
        public string Name { get; set; }
        public uint SetFlags { get; set; }
        public uint RequiredSkill { get; set; }
        public ushort RequiredSkillRank { get; set; }
        [HotfixArray(17)]
        public uint[] ItemID { get; set; }
    }
}
