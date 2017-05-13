using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.PvpTalent, HasIndexInData = false)]
    public class PvpTalentEntry
    {
        public uint SpellID { get; set; }
        public uint OverridesSpellID { get; set; }
        public string Description { get; set; }
        public uint TierID { get; set; }
        public uint ColumnIndex { get; set; }
        public uint Flags { get; set; }
        public uint ClassID { get; set; }
        public uint SpecID { get; set; }
        public uint Unknown { get; set; }
    }
}
