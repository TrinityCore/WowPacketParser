using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellProcsPerMinuteMod, HasIndexInData = false)]
    public class SpellProcsPerMinuteModEntry
    {
        public byte Type { get; set; }
        public int Param { get; set; }
        public float Coeff { get; set; }
        public ushort SpellProcsPerMinuteID { get; set; }
    }
}
