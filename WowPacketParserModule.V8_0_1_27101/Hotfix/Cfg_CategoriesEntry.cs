using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Cfg_Categories, HasIndexInData = false)]
    public class Cfg_CategoriesEntry
    {
        public string Name { get; set; }
        public ushort LocaleMask { get; set; }
        public byte CreateCharsetMask { get; set; }
        public byte ExistingCharsetMask { get; set; }
        public byte Flags { get; set; }
    }
}
