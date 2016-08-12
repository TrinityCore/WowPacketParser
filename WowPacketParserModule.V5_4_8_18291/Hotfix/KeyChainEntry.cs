using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_4_7_17898.Hotfix
{
    [HotfixStructure(DB2Hash.KeyChain)]
    public class KeyChainEntry
    {
        public int KeychainID { get; set; }
        [HotfixArray(32)]
        public byte[] Key { get; set; }
    }
}
