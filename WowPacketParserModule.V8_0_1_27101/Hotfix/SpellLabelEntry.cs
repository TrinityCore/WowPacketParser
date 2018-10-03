using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellLabel, HasIndexInData = false)]
    public class SpellLabelEntry
    {
        public uint LabelID { get; set; }
        public int SpellID { get; set; }
    }
}
