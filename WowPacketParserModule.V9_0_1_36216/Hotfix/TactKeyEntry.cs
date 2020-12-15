using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.TactKey, HasIndexInData = false)]
    public class TactKeyEntry
    {
        [HotfixArray(16)]
        public byte[] Key { get; set; }
    }
}
