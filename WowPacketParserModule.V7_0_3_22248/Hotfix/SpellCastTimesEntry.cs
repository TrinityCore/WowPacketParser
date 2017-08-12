using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellCastTimes, HasIndexInData = false)]
    public class SpellCastTimesEntry
    {
        public int CastTime { get; set; }
        public int MinCastTime { get; set; }
        public short CastTimePerLevel { get; set; }
    }
}