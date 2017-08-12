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
        public int TierID { get; set; }
        public int ColumnIndex { get; set; }
        public int Flags { get; set; }
        public int ClassID { get; set; }
        public int SpecID { get; set; }
        public int Role { get; set; }
    }
}
