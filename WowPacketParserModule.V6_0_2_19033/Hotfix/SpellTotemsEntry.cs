using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.SpellTotems)]
    public class SpellTotemsEntry
    {
        public uint ID { get; set; }
        [HotfixArray(2)]
        public uint[] RequiredTotemCategoryID { get; set; }
        [HotfixArray(2)]
        public uint[] Totem { get; set; }
    }
}