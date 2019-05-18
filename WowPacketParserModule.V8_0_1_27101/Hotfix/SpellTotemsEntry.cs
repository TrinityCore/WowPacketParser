using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellTotems, HasIndexInData = false)]
    public class SpellTotemsEntry
    {
        public int SpellID { get; set; }
        [HotfixArray(2)]
        public ushort[] RequiredTotemCategoryID { get; set; }
        [HotfixArray(2)]
        public int[] Totem { get; set; }
    }
}
