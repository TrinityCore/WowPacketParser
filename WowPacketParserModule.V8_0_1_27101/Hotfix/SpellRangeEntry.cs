using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellRange, HasIndexInData = false)]
    public class SpellRangeEntry
    {
        public string DisplayName { get; set; }
        public string DisplayNameShort { get; set; }
        public byte Flags { get; set; }
        [HotfixArray(2)]
        public float[] RangeMin { get; set; }
        [HotfixArray(2)]
        public float[] RangeMax { get; set; }
    }
}
