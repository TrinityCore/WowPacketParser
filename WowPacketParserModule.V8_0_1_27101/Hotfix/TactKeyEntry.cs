using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TactKey, HasIndexInData = false)]
    public class TactKeyEntry
    {
        [HotfixArray(16)]
        public byte[] Key { get; set; }
    }
}
