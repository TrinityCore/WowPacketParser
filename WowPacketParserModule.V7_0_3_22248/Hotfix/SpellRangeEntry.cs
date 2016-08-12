using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellRange, HasIndexInData = false)]
    public class SpellRangeEntry
    {
        public float MinRangeHostile { get; set; }
        public float MinRangeFriend { get; set; }
        public float MaxRangeHostile { get; set; }
        public float MaxRangeFriend { get; set; }
        public string DisplayName { get; set; }
        public string DisplayNameShort { get; set; }
        public byte Flags { get; set; }
    }
}