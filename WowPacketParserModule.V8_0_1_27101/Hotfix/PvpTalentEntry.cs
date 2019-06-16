using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PvpTalent)]
    public class PvpTalentEntry
    {
        public string Description { get; set; }
        public uint ID { get; set; }
        public int SpecID { get; set; }
        public int SpellID { get; set; }
        public int OverridesSpellID { get; set; }
        public int Flags { get; set; }
        public int ActionBarSpellID { get; set; }
        public int PvpTalentCategoryID { get; set; }
        public int LevelRequired { get; set; }
    }
}
