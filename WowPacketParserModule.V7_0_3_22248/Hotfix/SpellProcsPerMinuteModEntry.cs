using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellProcsPerMinuteMod, HasIndexInData = false)]
    public class SpellProcsPerMinuteModEntry
    {
        public float Coeff { get; set; }
        public ushort Param { get; set; }
        public byte Type { get; set; }
        public byte SpellProcsPerMinuteID { get; set; }
    }
}