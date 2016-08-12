using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellTotems, HasIndexInData = false)]
    public class SpellTotemsEntry
    {
        public uint SpellID { get; set; }
        [HotfixArray(2)]
        public uint[] Totem { get; set; }
        [HotfixArray(2)]
        public ushort[] RequiredTotemCategoryID { get; set; }
    }
}