using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellLabel, HasIndexInData = false)]
    public class SpellLabelEntry
    {
        public uint LabelID { get; set; }
        public uint SpellID { get; set; }
    }
}
