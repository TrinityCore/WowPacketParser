using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.KeyChain, HasIndexInData = false)]
    public class KeyChainEntry
    {
        [HotfixArray(32)]
        public byte[] Key { get; set; }
    }
}