using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Spell)]
    public class SpellEntry
    {
        public string Name { get; set; }
        public string NameSubtext { get; set; }
        public string Description { get; set; }
        public string AuraDescription { get; set; }
        public uint MiscID { get; set; }
        public uint ID { get; set; }
        public uint DescriptionVariablesID { get; set; }
    }
}