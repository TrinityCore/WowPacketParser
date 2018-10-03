using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellAuraVisXChrSpec, HasIndexInData = false)]
    public class SpellAuraVisXChrSpecEntry
    {
        public short ChrSpecializationID { get; set; }
        public short SpellAuraVisibilityID { get; set; }
    }
}
