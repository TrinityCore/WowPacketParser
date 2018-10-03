using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TactKeyLookup, HasIndexInData = false)]
    public class TactKeyLookupEntry
    {
        [HotfixArray(8)]
        public byte[] TACTID { get; set; }
    }
}
