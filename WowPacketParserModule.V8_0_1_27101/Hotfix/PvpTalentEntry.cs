using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PvpTalent)]
    public class PvpTalentEntry
    {
        public string Description { get; set; }
        public int ID { get; set; }
        public int SpecID { get; set; }
        public int SpellID { get; set; }
        public int OverridesSpellID { get; set; }
        public uint Flags { get; set; }
        public uint ActionBarSpellID { get; set; }
        public uint PvpTalentCategoryID { get; set; }
        public uint LevelRequired { get; set; }
    }
}
